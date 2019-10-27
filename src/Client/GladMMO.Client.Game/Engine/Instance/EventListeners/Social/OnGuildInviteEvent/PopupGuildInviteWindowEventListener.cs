using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PopupGuildInviteWindowEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<IGuildMemberInviteEventEventSubscribable, GenericSocialEventArgs<GuildMemberInviteEventModel>>
	{
		private IUIGuildInviteWindow GuildInviteWindow { get; }

		private INameQueryService NameQueryService { get; }

		private IEntityNameQueryable EntityNameQueryable { get; }

		private IRemoteSocialHubServer SocialHub { get; }

		public PopupGuildInviteWindowEventListener(IGuildMemberInviteEventEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.InvitedToGuildWindow)] [NotNull] IUIGuildInviteWindow guildInviteWindow, 
			INameQueryService nameQueryService,
			[NotNull] IEntityNameQueryable entityNameQueryable,
			[NotNull] IRemoteSocialHubServer socialHub) 
			: base(subscriptionService)
		{
			GuildInviteWindow = guildInviteWindow ?? throw new ArgumentNullException(nameof(guildInviteWindow));
			NameQueryService = nameQueryService;
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
			SocialHub = socialHub ?? throw new ArgumentNullException(nameof(socialHub));

			//Here we rig up the decline and accept invite buttons
			guildInviteWindow.AcceptInviteButton.AddOnClickListener(() => SocialHub.SendGuildInviteEventResponseAsync(new PendingGuildInviteHandleRequest(true)));
			guildInviteWindow.DeclineInviteButton.AddOnClickListener(() => SocialHub.SendGuildInviteEventResponseAsync(new PendingGuildInviteHandleRequest(false)));
		}

		protected override void OnThreadUnSafeEventFired(object source, GenericSocialEventArgs<GuildMemberInviteEventModel> args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				//We need to translate the names first.
				var nameQueryResponse = await NameQueryService.RetrieveGuildNameAsync(args.Data.GuildId)
					.ConfigureAwait(true);
				string inviterName = await EntityNameQueryable.RetrieveAsync(args.Data.InviterGuid)
					.ConfigureAwait(true);

				if (nameQueryResponse.isSuccessful)
				{
					GuildInviteWindow.GuildNameText.Text = $"<{nameQueryResponse.Result.EntityName}>";
					GuildInviteWindow.InvitationText.Text = $"<color=green><b>{inviterName}</b></color> invites you to join the guild:";

					//Now it can popup.
					GuildInviteWindow.SetElementActive(true);
				}
			});
		}
	}
}
