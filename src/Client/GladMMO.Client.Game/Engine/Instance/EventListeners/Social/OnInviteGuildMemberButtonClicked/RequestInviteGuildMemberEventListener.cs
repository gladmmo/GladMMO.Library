using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestInviteGuildMemberEventListener : ButtonClickedEventListener<IInviteGuildMemberModalClickedEventSubscribable>
	{
		private IUIButton InviteGuildMemberButton { get; }

		private IUIElement GuildInviteModalWindow { get; }

		private IUIText GuildInviteInputText { get; }

		private ILog Logger { get; }

		private IRemoteSocialHubServer RealtimeSocialConnection { get; }

		public RequestInviteGuildMemberEventListener(IInviteGuildMemberModalClickedEventSubscribable subscriptionService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.AddGuildMemberModalWindow)] IUIButton inviteGuildMemberButton,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.AddGuildMemberModalWindow)] IUIElement guildInviteModalWindow,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.AddGuildMemberModalWindow)] IUIText guildInviteInputText,
			[NotNull] ILog logger,
			[NotNull] IRemoteSocialHubServer realtimeSocialConnection) 
			: base(subscriptionService)
		{
			InviteGuildMemberButton = inviteGuildMemberButton ?? throw new ArgumentNullException(nameof(inviteGuildMemberButton));
			GuildInviteModalWindow = guildInviteModalWindow;
			GuildInviteInputText = guildInviteInputText ?? throw new ArgumentNullException(nameof(guildInviteInputText));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			RealtimeSocialConnection = realtimeSocialConnection ?? throw new ArgumentNullException(nameof(realtimeSocialConnection));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			//Do nothing, it's not valid.
			if (String.IsNullOrWhiteSpace(GuildInviteInputText.Text))
				return;

			//Disable the add button temporarily.
			InviteGuildMemberButton.IsInteractable = false;
			string memberToInvite = GuildInviteInputText.Text;

			//Try adding them
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await RealtimeSocialConnection.SendGuildInviteRequestAsync(new GuildMemberInviteRequestModel(memberToInvite));
				}
				catch (Exception e)
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Guild invite failed. Exception: {e.ToString()}");

					throw;
				}
				finally
				{
					//Close it either way, even if the add failed.
					//Then renable it for future use.
					GuildInviteModalWindow.SetElementActive(false);
					InviteGuildMemberButton.IsInteractable = true;
					GuildInviteInputText.Text = "";
				}
			});
		}
	}
}
