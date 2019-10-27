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
	//Depends on TWO events succeeding
	//IGuildStatusChangedEventSubscribable
	//IVoiceSessionAuthenticatedEventSubscribable
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class JoinGuildTextChatEventListener : OnLocalPlayerGuildStatusChangedEventListener
	{
		//It's a task because it MAY not be available as soon as the guild status changes.
		private Task<ILoginSession> ChatChannelSession { get; }

		private IVivoxAuthorizationService VivoxAutheAuthorizationService { get; }

		private IChatChannelJoinedEventPublisher ChannelJoinEventPublisher { get; }

		private ILog Logger { get; }

		public JoinGuildTextChatEventListener(IGuildStatusChangedEventSubscribable subscriptionService, 
			IReadonlyLocalPlayerDetails localPlayerDetails,
			IVoiceSessionAuthenticatedEventSubscribable vivoxAuthenticatedEventSubscribable,
			[NotNull] IVivoxAuthorizationService vivoxAutheAuthorizationService,
			[NotNull] IChatChannelJoinedEventPublisher channelJoinEventPublisher,
			[NotNull] ILog logger) 
			: base(subscriptionService, localPlayerDetails)
		{
			VivoxAutheAuthorizationService = vivoxAutheAuthorizationService ?? throw new ArgumentNullException(nameof(vivoxAutheAuthorizationService));
			ChannelJoinEventPublisher = channelJoinEventPublisher ?? throw new ArgumentNullException(nameof(channelJoinEventPublisher));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			TaskCompletionSource<ILoginSession> source = new TaskCompletionSource<ILoginSession>();
			ChatChannelSession = source.Task;

			//When vivox authenticates we can then set the completion of the login session.
			vivoxAuthenticatedEventSubscribable.OnVoiceSessionAuthenticated += (sender, args) => source.SetResult(args.Session);
		}

		protected override void OnGuildStatusChanged(GuildStatusChangedEventModel changeArgs)
		{
			//TODO: This needs to be authorative
			ProjectVersionStage.AssertBeta();
			//If we're now guildless, we need to actually leave the guild chat channel
			//if we're in one.
			if (changeArgs.IsGuildless)
			{
				//TODO: Handle leaving guild channel
				return;
			}

			//We have a guild, let's join the guild channel now.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					ResponseModel<VivoxChannelJoinResponse, VivoxLoginResponseCode> channelJoinResponse = await VivoxAutheAuthorizationService.JoinProximityChatAsync();

					//TODO: Better handle failure.
					if (!channelJoinResponse.isSuccessful)
					{
						if (Logger.IsErrorEnabled)
							Logger.Error($"Failed to join Guild world channel. Reason: {channelJoinResponse.ResultCode}");

						return;
					}

					if (Logger.IsInfoEnabled)
						Logger.Info($"Recieved Vivox Channel URI: {channelJoinResponse.Result.ChannelURI}");

					//TODO: Awaiting the ChatChannelSession may never complete. We should do a timeout.
					//TODO: We should share these inputs as shared constants.
					IChannelSession guildChannel = (await ChatChannelSession).GetChannelSession(new ChannelId(channelJoinResponse.Result.ChannelURI));

					await guildChannel.ConnectionAsync(false, true, TransmitPolicy.Yes, channelJoinResponse.Result.AuthToken)
						.ConfigureAwait(true);

					if(Logger.IsInfoEnabled)
						Logger.Info($"Joined Guild Chat");

					//Broadcast that we've joined proximity chat.
					ChannelJoinEventPublisher.PublishEvent(this, new ChatChannelJoinedEventArgs(ChatChannelType.Guild, new DefaultVivoxTextChannelSubscribableAdapter(guildChannel), new DefaultVivoxChatChannelSenderAdapter(guildChannel)));
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to Initialize Guild chat. Reason: {e.Message}\n\nStack: {e.StackTrace}");

					throw;
				}
			});
		}
	}
}
