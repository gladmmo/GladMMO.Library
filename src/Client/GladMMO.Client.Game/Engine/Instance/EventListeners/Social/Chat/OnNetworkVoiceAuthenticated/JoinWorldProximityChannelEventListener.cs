using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using VivoxUnity;

namespace GladMMO
{
	//TODO: Refactor with guild channel join.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class JoinWorldProximityChannelEventListener : BaseSingleEventListenerInitializable<IVoiceSessionAuthenticatedEventSubscribable, VoiceSessionAuthenticatedEventArgs>
	{
		private ILog Logger { get; }

		private IPositionalVoiceChannelCollection PositionalVoiceChannels { get; }

		private IVivoxAuthorizationService VivoxAutheAuthorizationService { get; }

		private IChatChannelJoinedEventPublisher ChannelJoinEventPublisher { get; }

		public JoinWorldProximityChannelEventListener(IVoiceSessionAuthenticatedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IPositionalVoiceChannelCollection positionalVoiceChannels,
			[NotNull] IVivoxAuthorizationService vivoxAutheAuthorizationService,
			[NotNull] IChatChannelJoinedEventPublisher channelJoinEventPublisher) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PositionalVoiceChannels = positionalVoiceChannels ?? throw new ArgumentNullException(nameof(positionalVoiceChannels));
			VivoxAutheAuthorizationService = vivoxAutheAuthorizationService ?? throw new ArgumentNullException(nameof(vivoxAutheAuthorizationService));
			ChannelJoinEventPublisher = channelJoinEventPublisher ?? throw new ArgumentNullException(nameof(channelJoinEventPublisher));
		}

		protected override void OnEventFired(object source, VoiceSessionAuthenticatedEventArgs args)
		{
			//Join the world channel with the session in the args.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					ResponseModel<VivoxChannelJoinResponse, VivoxLoginResponseCode> channelJoinResponse = await VivoxAutheAuthorizationService.JoinProximityChatAsync();

					//TODO: Better handle failure.
					if (!channelJoinResponse.isSuccessful)
					{
						if(Logger.IsErrorEnabled)
							Logger.Error($"Failed to join Proximity world channel. Reason: {channelJoinResponse.ResultCode}");

						return;
					}

					if(Logger.IsInfoEnabled)
						Logger.Info($"Recieved Vivox Channel URI: {channelJoinResponse.Result.ChannelURI}");

					//TODO: We should share these inputs as shared constants.
					//IChannelSession testChannel = args.Session.GetChannelSession(new ChannelId("vrguardian-vrg-dev", "lobby", "vdx5.vivox.com", ChannelType.Positional));
					IChannelSession testChannel = args.Session.GetChannelSession(new ChannelId(channelJoinResponse.Result.ChannelURI));

					//Prevent mobile platform connecting to audio
					await testChannel.ConnectionAsync(true, true, TransmitPolicy.Yes, channelJoinResponse.Result.AuthToken)
						.ConfigureAwait(true);

					//Documentation says that it doesn't mean the channel has connected yet.
					if(Logger.IsInfoEnabled)
						Logger.Info($"Vivox ChannelState: {testChannel.AudioState}");

					PositionalVoiceChannels.Add(testChannel);

					//Broadcast that we've joined proximity chat.
					ChannelJoinEventPublisher.PublishEvent(this, new ChatChannelJoinedEventArgs(ChatChannelType.Proximity, new DefaultVivoxTextChannelSubscribableAdapter(testChannel), new DefaultVivoxChatChannelSenderAdapter(testChannel)));
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
