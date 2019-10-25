using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestAddFriendEventListener : ButtonClickedEventListener<IAddFriendModalClickedEventSubscribable>
	{
		private IUIButton AddFriendButton { get; }

		private ISocialService SocialService { get; }

		private IUIElement FriendsAddModalWindow { get; }

		private IUIText FriendInputText { get; }

		private ILog Logger { get; }

		public RequestAddFriendEventListener(IAddFriendModalClickedEventSubscribable subscriptionService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.AddFriendModalWindow)] IUIButton addFriendButton,
			[NotNull] ISocialService socialService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.AddFriendModalWindow)] IUIElement friendsAddModalWindow,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.AddFriendModalWindow)] IUIText friendInputText,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			AddFriendButton = addFriendButton ?? throw new ArgumentNullException(nameof(addFriendButton));
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
			FriendsAddModalWindow = friendsAddModalWindow;
			FriendInputText = friendInputText ?? throw new ArgumentNullException(nameof(friendInputText));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			//Do nothing, it's not valid.
			if (String.IsNullOrWhiteSpace(FriendInputText.Text))
				return;

			//Disable the add button temporarily.
			AddFriendButton.IsInteractable = false;

			//Try adding them
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					var responseModel = await SocialService.TryAddFriendAsync(FriendInputText.Text);

					if (responseModel.isSuccessful)
					{
						if(Logger.IsInfoEnabled)
							Logger.Info($"Friend add successful. EntityId: {responseModel.Result.NewFriendEntityGuid}");
					}
					else
						if(Logger.IsWarnEnabled)
							Logger.Warn($"Friend add failed. Result Code: {responseModel.ResultCode}");
				}
				catch (Exception e)
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Friend add failed. Exception: {e.ToString()}");

					throw;
				}
				finally
				{
					//Close it either way, even if the add failed.
					//Then renable it for future use.
					FriendsAddModalWindow.SetElementActive(false);
					AddFriendButton.IsInteractable = true;
					FriendInputText.Text = "";
				}
			});
		}
	}
}
