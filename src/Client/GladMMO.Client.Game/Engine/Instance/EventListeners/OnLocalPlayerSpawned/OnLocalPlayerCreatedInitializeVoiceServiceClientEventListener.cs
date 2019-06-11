using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using VivoxUnity;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IVoiceNetworkInitializedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnLocalPlayerCreatedInitializeVoiceServiceClientEventListener : BaseSingleEventListenerInitializable<ILocalPlayerSpawnedEventSubscribable, LocalPlayerSpawnedEventArgs>, IVoiceNetworkInitializedEventSubscribable
	{
		private VivoxUnity.Client VoiceClient { get; }

		private ILog Logger { get; }

		public event EventHandler OnVoiceNetworkInitialized;

		public OnLocalPlayerCreatedInitializeVoiceServiceClientEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] VivoxUnity.Client voiceClient,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			VoiceClient = voiceClient ?? throw new ArgumentNullException(nameof(voiceClient));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, LocalPlayerSpawnedEventArgs args)
		{
			//It may seem small and simple, or dumb to seperate all this stuff, but a lot of stuff is going to happen with voice related events.
			VoiceClient.Initialize();
			OnVoiceNetworkInitialized?.Invoke(this, EventArgs.Empty);
		}
	}
}
