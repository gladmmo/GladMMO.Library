using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class VivoxDisconnectOnNetworkDisconnectionEventListener : BaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>
	{
		private VivoxUnity.Client VoiceClient { get; }

		public VivoxDisconnectOnNetworkDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService,
			[NotNull] VivoxUnity.Client voiceClient) : base(subscriptionService)
		{
			VoiceClient = voiceClient ?? throw new ArgumentNullException(nameof(voiceClient));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			VoiceClient.Uninitialize();
		}
	}
}
