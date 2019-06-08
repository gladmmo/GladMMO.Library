using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatWindowInputController : IGameInitializable
	{
		private IUIButton ChatInputButton { get; }

		private IUIText ChatInputText { get; }

		//private IRemoteSocialTextChatHubServer ChatService { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public ChatWindowInputController(
			[KeyFilter(UnityUIRegisterationKey.ChatBox)] [NotNull] IUIButton chatInputButton,
			[KeyFilter(UnityUIRegisterationKey.ChatBox)] [NotNull] IUIText chatInputText,
			//[NotNull] IRemoteSocialTextChatHubServer chatService,
			[NotNull] ILog logger)
		{
			ChatInputButton = chatInputButton ?? throw new ArgumentNullException(nameof(chatInputButton));
			ChatInputText = chatInputText ?? throw new ArgumentNullException(nameof(chatInputText));
			//ChatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public async Task OnGameInitialized()
		{
			//Must be on the mainthread
			await new UnityYieldAwaitable();

			//Allow the user to chat
			ChatInputButton.IsInteractable = true;

			ChatInputButton.AddOnClickListener(() => { ChatInputButton.IsInteractable = false; });

			//This ALWAYS runs after the sync so it's safe to renable here after it's done
			ChatInputButton.AddOnClickListenerAsync(async () =>
			{
				await OnChatButtonClicked();
			});
		}

		private async Task OnChatButtonClicked()
		{
			try
			{
				//TODO: This is kinda a hack, just for testing purposes.
				throw new NotImplementedException($"Not implemented chat again yet.");
				/*if(ChatInputText.Text.Contains("/guild "))
				{
					string strippedChat = ChatInputText.Text.Replace("/guild ", "");

					await ChatService.SendGuildChannelTextChatMessageAsync(new GuildChatMessageRequestModel(strippedChat))
						.ConfigureAwait(true);
				}
				else
					await ChatService.SendZoneChannelTextChatMessageAsync(new ZoneChatMessageRequestModel(ChatInputText.Text))
						.ConfigureAwait(true);*/
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to send ChatMessage. Exception: {e.Message}\n\nStack: {e.StackTrace}");

				//Don't throw, we need to just renable and HOPE
			}
			finally
			{
				//TODO: Do we need to rejoin the unity thread??
				await new UnityYieldAwaitable();
			}

			//Clear and renable chat.
			ChatInputText.Text = "";
			ChatInputButton.IsInteractable = true;
		}
	}
}
