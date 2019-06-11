using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using VivoxUnity;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PositionalVoiceAudioGameTickable : BaseSingleEventListenerInitializable<IVoiceSessionAuthenticatedEventSubscribable, VoiceSessionAuthenticatedEventArgs>, IGameTickable
	{
		[Flags]
		public enum PositionalAudioState : int
		{
			Default = 0,
			VoiceAuthenticated = 1 << 0,
			PlayerSpawned = 1 << 1,

			Running = PlayerSpawned | VoiceAuthenticated
		}

		public PositionalAudioState State { get; private set; } = PositionalAudioState.Default;

		private PositionalVoiceEntityTickable PositionalChannelUpdateTickable { get; }

		public void Tick()
		{
			//Sadly, has flags is implemented with reflection so don't do that in a loop.
			//When the player is spawned and voice is authenticated we can start servicing these channels.
			if ((State & PositionalAudioState.Running) == PositionalAudioState.Running)
				PositionalChannelUpdateTickable.Tick();
		}

		public PositionalVoiceAudioGameTickable(IVoiceSessionAuthenticatedEventSubscribable subscriptionService, 
			[NotNull] ILocalPlayerSpawnedEventSubscribable playerSpawnedSubscriptionService,
			[NotNull] PositionalVoiceEntityTickable positionalChannelUpdateTickable) 
			: base(subscriptionService)
		{
			if (playerSpawnedSubscriptionService == null) throw new ArgumentNullException(nameof(playerSpawnedSubscriptionService));
			PositionalChannelUpdateTickable = positionalChannelUpdateTickable ?? throw new ArgumentNullException(nameof(positionalChannelUpdateTickable));

			playerSpawnedSubscriptionService.OnLocalPlayerSpawned += OnLocalPlayerSpawnedEvent;
		}

		private void OnLocalPlayerSpawnedEvent(object sender, LocalPlayerSpawnedEventArgs e)
		{
			State |= PositionalAudioState.PlayerSpawned;

			//Now is the time to initialize the actual
			//local player's positional information.
			PositionalChannelUpdateTickable.InitializeTrackerGameObject(e.EntityGuid);
		}

		protected override void OnEventFired(object source, VoiceSessionAuthenticatedEventArgs args)
		{
			State |= PositionalAudioState.VoiceAuthenticated;
		}
	}

	public sealed class PositionalVoiceEntityTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> GameObjectDirectoryMappable { get; }

		/// <summary>
		/// The 3D voice tracker.
		/// </summary>
		public Transform TrackerObject { get; private set; }

		private IReadonlyPositionalVoiceChannelCollection PositionalChannels { get; }

		public PositionalVoiceEntityTickable([NotNull] IReadonlyPositionalVoiceChannelCollection positionalChannels, [NotNull] IReadonlyEntityGuidMappable<EntityGameObjectDirectory> gameObjectDirectoryMappable)
		{
			PositionalChannels = positionalChannels ?? throw new ArgumentNullException(nameof(positionalChannels));
			GameObjectDirectoryMappable = gameObjectDirectoryMappable ?? throw new ArgumentNullException(nameof(gameObjectDirectoryMappable));
		}

		public void InitializeTrackerGameObject(NetworkEntityGuid entityGuid)
		{
			//Cache the gameobject that trackers voice.
			TrackerObject = GameObjectDirectoryMappable.RetrieveEntity(entityGuid).GetGameObject(EntityGameObjectDirectory.Type.CameraRoot).transform;
		}

		public void Tick()
		{
			if(TrackerObject == null)
				throw new InvalidOperationException($"Failed to run positional voice. Tracker was null.");

			foreach(var channel in PositionalChannels)
			{
				//TODO: We should handle mouth and ears seperately.
				if(channel.AudioState == ConnectionState.Connected)
					channel.Set3DPosition(TrackerObject.position, TrackerObject.position, TrackerObject.forward, TrackerObject.up);
			}
		}
	}
}
