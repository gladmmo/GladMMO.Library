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
	//TODO: Rename, it's based on local player spawn event now.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnLocalPlayerCreatedConnectVoiceServiceClientEventListener : BaseSingleEventListenerInitializable<ILocalPlayerSpawnedEventSubscribable, LocalPlayerSpawnedEventArgs>, IGameTickable
	{
		private VivoxUnity.Client VoiceClient { get; }

		private ILog Logger { get; }

		private bool isClientInitialized { get; set; } = false;

		public OnLocalPlayerCreatedConnectVoiceServiceClientEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] VivoxUnity.Client voiceClient,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			VoiceClient = voiceClient ?? throw new ArgumentNullException(nameof(voiceClient));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, LocalPlayerSpawnedEventArgs args)
		{
			//TODO: This is due to Unity Editor issues I think.
			VoiceClient.Initialize();
			isClientInitialized = true;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					ProjectVersionStage.AssertBeta();
					//TODO: We should use authorative serverside authorization/tokens.
					ILoginSession session = VoiceClient.GetLoginSession(new AccountId("vrguardian-vrg-dev", args.EntityGuid.EntityId.ToString() , "vdx5.vivox.com"));

					await session.LoginAsync(new Uri("https://vdx5.www.vivox.com/api2"), session.GetLoginToken(VivoxDemoAPIKey.AccessKey, TimeSpan.FromSeconds(90)))
						.ConfigureAwait(true);

					if(Logger.IsInfoEnabled)
						Logger.Info($"Vivox LoginState: {session.State}");

					//TODO: We should use channel based on zoneId.
					IChannelSession testChannel = session.GetChannelSession(new ChannelId("vrguardian-vrg-dev", "lobby", "vdx5.vivox.com", ChannelType.Echo));

					await testChannel.ConnectionAsync(true, false, TransmitPolicy.Yes, testChannel.GetConnectToken(VivoxDemoAPIKey.AccessKey, TimeSpan.FromSeconds(90)))
						.ConfigureAwait(true);

					if(Logger.IsInfoEnabled)
						Logger.Info($"Vivox ChannelState: {testChannel.AudioState}");
				}
				catch (Exception e)
				{
					Logger.Error($"Failed to Initialize Vivox Voice. Reason: {e.Message}\n\nStack: {e.StackTrace}");
					throw;
				}
			});
		}

		/*foreach (var channel in _voiceServer.PositionalChannels)
		{
			var positionalChannelSession = _client.GetUser(_username).GetChannelSession(channel);
			positionalChannelSession.Set3DPosition(speaker.position, listener.position, listener.forward, listener.up);
		}*/

		//It's important that this runs, because it's the message pump that services things I guess.
		public void Tick()
		{
			try
			{
				//TODO: Maybe hide this behind an extension
				if (isClientInitialized)
					VivoxUnity.Client.RunOnce();
			}
			catch(Exception e)
			{
				Logger.Error($"Vivox Error: {e.Message}\n\nStack: {e.StackTrace}");
			}
		}
	}
}
