using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	//[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatInputEventRegisterationEventListener : IGameInitializable
	{
		private IUIButton ChatEnterButton { get; }

		private IUIText ChatEnterText { get; }

		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }
		
		private ILog Logger { get; }

		/// <inheritdoc />
		public ChatInputEventRegisterationEventListener(
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ChatInput)] IUIButton chatEnterButton,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ChatInput)] IUIText chatEnterText,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService,
			[NotNull] ILog logger)
		{
			ChatEnterButton = chatEnterButton ?? throw new ArgumentNullException(nameof(chatEnterButton));
			ChatEnterText = chatEnterText ?? throw new ArgumentNullException(nameof(chatEnterText));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			ChatEnterButton.AddOnClickListener(OnChatMessageEnterPressed);
			return Task.CompletedTask;
		}

		private void OnChatMessageEnterPressed()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Attempting to send ChatMessage: {ChatEnterText.Text}");

			//TODO: Is this possible? Since I know InputField force clicks the button?
			if(!ChatEnterButton.IsInteractable)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Button pressed event fired when Button: {nameof(ChatEnterButton)} {nameof(ChatEnterButton.IsInteractable)} was false.");
				return;
			}

			//We shouldn't send nothing.
			if(String.IsNullOrWhiteSpace(ChatEnterText.Text))
				return;

			ChatEnterButton.IsInteractable = false;

			//Once pressed, we should just send the chat message.
			//But we send async soooo, let's not block the caller and not assume it happens instantly.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					//TODO: Renable group invite testing command.
					/*if(ChatEnterText.Text.Contains("/invite"))
					{
						string toInvite = ChatEnterText.Text.Split(' ').Last();
						if(Logger.IsDebugEnabled)
							Logger.Debug($"About to invite: {toInvite}");

						await SendService.SendMessage(new ClientGroupInviteRequest(toInvite));
					}
					else
						await SendService.SendMessage(new ChatMessageRequest(new SayPlayerChatMessage(ChatLanguage.LANG_ORCISH, ChatEnterText.Text)))
							.ConfigureAwait(true);*/

					//We clear text here because we actually DON'T wanna clear the text if there was an error.
					ChatEnterText.Text = "";
				}
				catch(Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Could not send chat message. Exception {e.GetType().Name}: {e.Message}\n\nStackTrace:{e.StackTrace}");

					throw;
				}
				finally
				{
					ChatEnterButton.IsInteractable = true;
				}
			});
		}
	}
}
