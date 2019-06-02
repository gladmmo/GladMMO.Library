using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;
using UnityEngine.Events;

namespace GladMMO
{
	[GameInitializableOrdering(Int32.MaxValue)] //this should ALWAYS be last, just incase anything else is in queue.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerEntityExitManager : EventQueueBasedTickable<ISessionDisconnectionEventSubscribable, SessionStatusChangeEventArgs>
	{
		/// <summary>
		/// The deconstrutor for the session.
		/// </summary>
		private IObjectDestructorable<PlayerSessionDeconstructionContext> SessionDestructor { get; }

		/// <summary>
		/// The zone client game service.
		/// </summary>
		private IZoneServerToGameServerClient ZoneClientGameService { get; }

		private IConnectionEntityCollection ConnectionToEntityMap { get; }

		private IEntityDataSaveable EntitySaver { get; }

		/// <summary>
		/// The collections locking policy.
		/// </summary>
		private GlobalEntityCollectionsLockingPolicy LockingPolicy { get; }

		/// <inheritdoc />
		public PlayerEntityExitManager(
			[NotNull] ISessionDisconnectionEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IObjectDestructorable<PlayerSessionDeconstructionContext> sessionDestructor,
			[NotNull] IZoneServerToGameServerClient zoneClientGameService,
			[NotNull] IConnectionEntityCollection connectionToEntityMap,
			[NotNull] GlobalEntityCollectionsLockingPolicy lockingPolicy,
			[NotNull] IEntityDataSaveable entitySaver) 
			: base(subscriptionService, false, logger)
		{
			SessionDestructor = sessionDestructor ?? throw new ArgumentNullException(nameof(sessionDestructor));
			ZoneClientGameService = zoneClientGameService ?? throw new ArgumentNullException(nameof(zoneClientGameService));
			ConnectionToEntityMap = connectionToEntityMap ?? throw new ArgumentNullException(nameof(connectionToEntityMap));
			LockingPolicy = lockingPolicy ?? throw new ArgumentNullException(nameof(lockingPolicy));
			EntitySaver = entitySaver ?? throw new ArgumentNullException(nameof(entitySaver));
		}

		/// <inheritdoc />
		protected override void HandleEvent(SessionStatusChangeEventArgs args)
		{
			//The reason this system is so simple is basically the other threads just need a way to queue up
			//cleanup on the main thread. The reasoning being that GameObject destruction needs to occur
			//as well as collection modification needs to happen, and the main thread is where the majority if collection
			//iteration should be taking place.

			//it's possible that we're attempting to clean up an entity for a connection
			//that doesn't have one. This can happen if they disconnect during claim or before claim.
			if(!ConnectionToEntityMap.ContainsKey(args.Details.ConnectionId))
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"ConnectionId: {args.Details.ConnectionId} had entity exit cleanup but contained no entity. This is not an error.");

				//We may be in this method, handling cleanup for an entity that has a claim going on
				//in the claim handler, but is still awaiting a response for character data from the gameserver. MEANING we could end up with hanging entites.
				//This is not mitgated here, but inside the player entery factory
				//which SHOULD make a check for the connection still being valid AFTER creation
				//Not before because we still want to create, and then deconstruct. Reasoning being that gameserver session
				//will still be claimed unless we go through th cleanup process.
				return;
			}

			NetworkEntityGuid entityGuid = ConnectionToEntityMap[args.Details.ConnectionId];

			//First we need to save the entity data since it DOES own an entity.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				//Nothing should really be taking a write lock, except session entry I guess.
				//This could be bad
				//We no longer do a read lock because Player Entry is actually trying to
				//aquire a write lock and this could block if for a LONG time. KILLING the server basically
				//So, we just assume it's safe to save entity data. Even if it's changing mid save like this.
				//using(await LockingPolicy.ReaderLockAsync(null, CancellationToken.None))
				{
					//We know that an entity exists, so we must save it. Before we even queue it up for removal.
					await EntitySaver.SaveAsync(entityGuid)
						.ConfigureAwait(true);
				}

				//At this point we MUST write lock, since we are actually modifying entity collections and entries
				using(await LockingPolicy.WriterLockAsync(null, CancellationToken.None)) //we can use async await since we're in a async context too!! Which is good.
				{
					SessionDestructor.Destroy(new PlayerSessionDeconstructionContext(args.Details.ConnectionId));
				}

				//TODO: We have a big problem if this fails, we need to handle it properly. Otherwise the player cannot log in again.\
				ProjectVersionStage.AssertBeta();

				//We need to async send the release request, a very important part of session cleanup.
				//if this failes we have BIG problems. BIG BIG BIG.
				await ZoneClientGameService.ReleaseActiveSession(entityGuid.EntityId)
					.ConfigureAwait(false);

				if(Logger.IsInfoEnabled)
					Logger.Info($"Cleaned up Entity Player Session for ConnectionId: {args.Details.ConnectionId} Guid: {entityGuid} {entityGuid.EntityType}:{entityGuid.EntityId}");
			});
		}
	}
}
