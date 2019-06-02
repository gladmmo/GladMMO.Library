using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IPlayerWorldSessionCreatedEventSubscribable))]
	[GameInitializableOrdering(2)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerEntityEntryManager : EventQueueBasedTickable<IPlayerSessionClaimedEventSubscribable, PlayerSessionClaimedEventArgs>, IPlayerWorldSessionCreatedEventSubscribable
	{
		private IFactoryCreatable<GameObject, PlayerEntityCreationContext> PlayerFactory { get; }

		private INetworkMessageSender<GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>> SpawnPayloadSender { get; }

		/// <inheritdoc />
		public event EventHandler<PlayerWorldSessionCreationEventArgs> OnPlayerWorldSessionCreated;

		/// <summary>
		/// The collections locking policy.
		/// </summary>
		private GlobalEntityCollectionsLockingPolicy LockingPolicy { get; }

		/// <inheritdoc />
		public PlayerEntityEntryManager(
			IPlayerSessionClaimedEventSubscribable subscriptionService,
			ISessionDisconnectionEventSubscribable disconnectionSubscriptionService,
			IFactoryCreatable<GameObject, PlayerEntityCreationContext> playerFactory, 
			INetworkMessageSender<GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>> spawnPayloadSender, 
			ILog logger,
			GlobalEntityCollectionsLockingPolicy lockingPolicy) 
			: base(subscriptionService, false, logger)
		{
			PlayerFactory = playerFactory;
			SpawnPayloadSender = spawnPayloadSender;
			LockingPolicy = lockingPolicy;

			disconnectionSubscriptionService.OnSessionDisconnection += OnSessionDisconnection;
		}

		private void OnSessionDisconnection(object sender, SessionStatusChangeEventArgs e)
		{
			//DO NOT REMOVE. This helps cover race conditions with handling entry/exit from the server.
			//Remove anything in the entry queue that is related to the connection id disconnecting
			//This is efficient BECAUSE: Likely case is queue size is 0. Likely case is it's also not in the queue.
			this.RemoveEventMatchingPredicate(predicateInput => predicateInput.SessionContext.ConnectionId == e.Details.ConnectionId);
		}

		/// <inheritdoc />
		protected override void HandleEvent(PlayerSessionClaimedEventArgs args)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Dequeueing entity creation request for: {args.EntityGuid.EntityType}:{args.EntityGuid.EntityId}");

			//TODO: This is test data
			EntityFieldDataCollection<EUnitFields> testData = new EntityFieldDataCollection<EUnitFields>();

			//TODO: Test values set
			testData.SetFieldValue(EUnitFields.UNIT_FIELD_HEALTH, 100);
			testData.SetFieldValue(EUnitFields.UNIT_FIELD_MAXHEALTH, 120);

			using(LockingPolicy.WriterLock(null, CancellationToken.None))
			{
				//TODO: Time stamp
				//TODO: We should check if the result is valid? Maybe return a CreationResult?
				//We don't need to do anything with the returned object.
				GameObject playerGameObject = PlayerFactory.Create(new PlayerEntityCreationContext(args.EntityGuid, args.SessionContext, EntityPrefab.RemotePlayer, args.SpawnPosition, 0));
			}

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Sending player spawn payload Id: {args.EntityGuid.EntityId}");

			OnPlayerWorldSessionCreated?.Invoke(this, new PlayerWorldSessionCreationEventArgs(args.EntityGuid));

			//TODO: If we want to do anything post-creation with the provide gameobject we could. But we really don't want to at the moment.

		}
	}
}
