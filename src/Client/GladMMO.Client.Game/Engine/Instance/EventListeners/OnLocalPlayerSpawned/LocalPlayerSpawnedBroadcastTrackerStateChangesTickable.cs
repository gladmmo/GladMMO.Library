using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class LocalPlayerSpawnedBroadcastTrackerStateChangesTickable : BaseSingleEventListenerInitializable<ILocalPlayerSpawnedEventSubscribable, LocalPlayerSpawnedEventArgs>, IGameTickable
	{
		private bool isLocalPlayerSpawned { get; set; } = false;

		private NetworkMovementTrackerTypeFlags ChangedTrackers { get; set; } = NetworkMovementTrackerTypeFlags.None;

		private IFactoryCreatable<PlayerNetworkTrackerChangeUpdateRequest, NetworkMovementTrackerTypeFlags> NetworkTrackerUpdateFactory { get; }

		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		public LocalPlayerSpawnedBroadcastTrackerStateChangesTickable(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			[NotNull] ICameraInputChangedEventSubscribable cameraInputSubscriptionService,
			[NotNull] IFactoryCreatable<PlayerNetworkTrackerChangeUpdateRequest, NetworkMovementTrackerTypeFlags> networkTrackerUpdateFactory,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService) 
			: base(subscriptionService)
		{
			if (cameraInputSubscriptionService == null) throw new ArgumentNullException(nameof(cameraInputSubscriptionService));

			NetworkTrackerUpdateFactory = networkTrackerUpdateFactory ?? throw new ArgumentNullException(nameof(networkTrackerUpdateFactory));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			cameraInputSubscriptionService.OnCameraInputChange += OnCameraInputChanged;
		}

		private void OnCameraInputChanged(object sender, EventArgs e)
		{
			//If we aren't spawned, we aren't ready yet.
			if(!isLocalPlayerSpawned)
				return;

			//This is the event callback for when ever ANY trackers change.
			ChangedTrackers |= NetworkMovementTrackerTypeFlags.Head;
		}

		protected override void OnEventFired(object source, LocalPlayerSpawnedEventArgs args)
		{
			isLocalPlayerSpawned = true;
		}

		public void Tick()
		{
			if (!isLocalPlayerSpawned)
				return;

			if (ChangedTrackers == NetworkMovementTrackerTypeFlags.None)
				return;

			PlayerNetworkTrackerChangeUpdateRequest changeUpdateRequest = NetworkTrackerUpdateFactory.Create(ChangedTrackers);

			SendService.SendMessage(changeUpdateRequest);

			ChangedTrackers = NetworkMovementTrackerTypeFlags.None;
		}
	}
}
