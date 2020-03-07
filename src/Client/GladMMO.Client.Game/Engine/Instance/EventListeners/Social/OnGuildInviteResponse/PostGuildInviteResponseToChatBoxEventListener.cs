using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PostGuildInviteResponseToChatBoxEventListener : BaseSingleEventListenerInitializable<IGuildInviteResponseEventSubscribable, GenericSocialEventArgs<GuildMemberInviteResponseModel>>
	{
		private IEntityNameQueryable EntityNameQueryable { get; }

		private IChatTextMessageRecievedEventPublisher TextChatPublisher { get; }

		public PostGuildInviteResponseToChatBoxEventListener(IGuildInviteResponseEventSubscribable subscriptionService,
			[NotNull] IEntityNameQueryable entityNameQueryable,
			[NotNull] IChatTextMessageRecievedEventPublisher textChatPublisher) 
			: base(subscriptionService)
		{
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
			TextChatPublisher = textChatPublisher ?? throw new ArgumentNullException(nameof(textChatPublisher));
		}

		protected override void OnEventFired(object source, GenericSocialEventArgs<GuildMemberInviteResponseModel> args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				string inviterName = args.Data.InvitedEntityGuid == ObjectGuid.Empty ? "" : await EntityNameQueryable.RetrieveAsync(args.Data.InvitedEntityGuid)
					.ConfigureAwait(true);

				//Only certain response codes actually need to be logged.
				switch (args.Data.ResultCode)
				{
					case GuildMemberInviteResponseCode.GeneralServerError:
						TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"Failed to invite to guild due to server error.", ChatChannelType.System));
						break;
					case GuildMemberInviteResponseCode.PlayerAlreadyInGuild:
						TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"{inviterName} is already in a guild.", ChatChannelType.System));
						break;
					case GuildMemberInviteResponseCode.PlayerDeclinedGuildInvite:
						TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"{inviterName} declined your invite to join the guild.", ChatChannelType.System));
						break;
					case GuildMemberInviteResponseCode.PlayerAlreadyHasPendingInvite:
						TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"{inviterName} already has a pending invite to join a guild.", ChatChannelType.System));
						break;
					case GuildMemberInviteResponseCode.PlayerNotFound:
						TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"Unable to invite player to guild; they either don't exist or are not online.", ChatChannelType.System));
						break;
				}
			});
		}
	}
}
