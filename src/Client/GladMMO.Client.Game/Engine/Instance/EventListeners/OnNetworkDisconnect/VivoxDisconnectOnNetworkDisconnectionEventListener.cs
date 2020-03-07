using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class VivoxDisconnectOnNetworkDisconnectionEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>
	{
		private VivoxUnity.Client VoiceClient { get; }

		public VivoxDisconnectOnNetworkDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService,
			[NotNull] VivoxUnity.Client voiceClient) : base(subscriptionService)
		{
			VoiceClient = voiceClient ?? throw new ArgumentNullException(nameof(voiceClient));
		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			//TODO: Do potential remote cleanup for client. Like saying "Hey, not in this channel maybe"
			//But we DO NOT need to uninitialize. AutoFac will call Dispose on the VivoxClient.
			//Meaning uninitialize will be called. If you call it AGAIN the client/editor actually CRASHES
			//so do not do this.
			//VoiceClient.Uninitialize();
		}
	}
}
