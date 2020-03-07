using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PostGuildInviteToChatBoxEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<IGuildMemberInviteEventEventSubscribable, GenericSocialEventArgs<GuildMemberInviteEventModel>>
	{
		private INameQueryService NameQueryService { get; }

		private IEntityNameQueryable EntityNameQueryable { get; }

		private IChatTextMessageRecievedEventPublisher TextChatPublisher { get; }

		public PostGuildInviteToChatBoxEventListener(IGuildMemberInviteEventEventSubscribable subscriptionService,
			INameQueryService nameQueryService,
			[NotNull] IEntityNameQueryable entityNameQueryable,
			[NotNull] IChatTextMessageRecievedEventPublisher textChatPublisher) 
			: base(subscriptionService)
		{
			NameQueryService = nameQueryService;
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
			TextChatPublisher = textChatPublisher ?? throw new ArgumentNullException(nameof(textChatPublisher));
		}

		protected override void OnThreadUnSafeEventFired(object source, GenericSocialEventArgs<GuildMemberInviteEventModel> args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				//TODO: We need a better way to handle guild query.
				//We need to translate the names first.
				var nameQueryResponse = await NameQueryService.RetrieveGuildNameAsync(args.Data.GuildId)
					.ConfigureAwait(true);
				string inviterName = await EntityNameQueryable.RetrieveAsync(args.Data.InviterGuid)
					.ConfigureAwait(true);

				TextChatPublisher.PublishEvent(this, new TextChatEventArgs($"{inviterName} invited you to join the guild <{nameQueryResponse.Result.EntityName}>.", ChatChannelType.System));
			});
		}
	}
}
