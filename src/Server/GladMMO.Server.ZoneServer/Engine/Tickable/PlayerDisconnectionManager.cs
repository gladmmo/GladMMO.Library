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
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerDisconnectionManager : EventQueueBasedTickable<ISessionDisconnectionEventSubscribable, SessionStatusChangeEventArgs>
	{
		private IConnectionEntityCollection ConnectionToEntityMap { get; }

		private IEventPublisher<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs> EntityDeconstructionRequestPublisher { get; }

		/// <inheritdoc />
		public PlayerDisconnectionManager(
			[NotNull] ISessionDisconnectionEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IConnectionEntityCollection connectionToEntityMap,
			[NotNull] IEventPublisher<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs> entityDeconstructionRequestPublisher)
			: base(subscriptionService, false, logger)
		{
			ConnectionToEntityMap = connectionToEntityMap ?? throw new ArgumentNullException(nameof(connectionToEntityMap));
			EntityDeconstructionRequestPublisher = entityDeconstructionRequestPublisher ?? throw new ArgumentNullException(nameof(entityDeconstructionRequestPublisher));
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
			else
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Cleaning up Entity for ConnectionId: {args.Details.ConnectionId}");

				EntityDeconstructionRequestPublisher.PublishEvent(this, new EntityDeconstructionRequestedEventArgs(ConnectionToEntityMap[args.Details.ConnectionId]));
			}
		}
	}
}
