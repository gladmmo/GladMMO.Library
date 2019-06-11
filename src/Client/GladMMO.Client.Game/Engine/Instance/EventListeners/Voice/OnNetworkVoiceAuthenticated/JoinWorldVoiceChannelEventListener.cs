using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using VivoxUnity;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class JoinWorldVoiceChannelEventListener : BaseSingleEventListenerInitializable<IVoiceSessionAuthenticatedEventSubscribable, VoiceSessionAuthenticatedEventArgs>
	{
		private ILog Logger { get; }

		private IPositionalVoiceChannelCollection PositionalVoiceChannels { get; }

		public JoinWorldVoiceChannelEventListener(IVoiceSessionAuthenticatedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IPositionalVoiceChannelCollection positionalVoiceChannels) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PositionalVoiceChannels = positionalVoiceChannels ?? throw new ArgumentNullException(nameof(positionalVoiceChannels));
		}

		protected override void OnEventFired(object source, VoiceSessionAuthenticatedEventArgs args)
		{
			//Join the world channel with the session in the args.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					//TODO: Make 3D positional audio.
					//TODO: We should use channel based on zoneId.
					IChannelSession testChannel = args.Session.GetChannelSession(new ChannelId("vrguardian-vrg-dev", "lobby", "vdx5.vivox.com", ChannelType.Positional));

					await testChannel.ConnectionAsync(true, false, TransmitPolicy.Yes, testChannel.GetConnectToken(VivoxDemoAPIKey.AccessKey, TimeSpan.FromSeconds(90)))
						.ConfigureAwait(true);

					//Documentation says that it doesn't mean the channel has connected yet.
					if(Logger.IsInfoEnabled)
						Logger.Info($"Vivox ChannelState: {testChannel.AudioState}");

					PositionalVoiceChannels.Add(testChannel);
				}
				catch(Exception e)
				{
					Logger.Error($"Failed to Initialize Vivox Voice. Reason: {e.Message}\n\nStack: {e.StackTrace}");
					throw;
				}
			});
		}

		//Notes on positional audio
		/*foreach (var channel in _voiceServer.PositionalChannels)
		{
			var positionalChannelSession = _client.GetUser(_username).GetChannelSession(channel);
			positionalChannelSession.Set3DPosition(speaker.position, listener.position, listener.forward, listener.up);
		}*/
	}
}
