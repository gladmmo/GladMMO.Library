using System; using FreecraftCore;
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
	[AdditionalRegisterationAs(typeof(IChatTextMessageEnteredEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatInputEventRegisterationEventListener : BaseSingleEventListenerInitializable<IVoiceSessionAuthenticatedEventSubscribable, VoiceSessionAuthenticatedEventArgs>, IChatTextMessageEnteredEventSubscribable
	{
		public event EventHandler<ChatTextMessageEnteredEventArgs> OnChatMessageEntered;

		private IUIButton ChatEnterButton { get; }

		private IUIText ChatEnterText { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public ChatInputEventRegisterationEventListener(IVoiceSessionAuthenticatedEventSubscribable subscriptionService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ChatInput)] IUIButton chatEnterButton,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ChatInput)] IUIText chatEnterText,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			ChatEnterButton = chatEnterButton ?? throw new ArgumentNullException(nameof(chatEnterButton));
			ChatEnterText = chatEnterText ?? throw new ArgumentNullException(nameof(chatEnterText));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			//TODO: We disabled waiting for channel auth specificially for PSOBB integration reasons. Not sure it's a good idea.
			ChatEnterButton.AddOnClickListener(OnChatMessageEnterPressed);
		}

		//Once voice is authenticated we can start listening to and trying to send text messages.
		protected override void OnEventFired(object source, VoiceSessionAuthenticatedEventArgs args)
		{
			//ChatEnterButton.AddOnClickListener(OnChatMessageEnterPressed);
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
			try
			{
				ChatChannelType type = ChatChannelType.Proximity;
				string chatMessage = ChatEnterText.Text;

				//If the first character is a slash
				//then they may want a specific channel
				if (ChatEnterText.Text[0] == '/')
				{
					string inputType = ChatEnterText.Text.Split(' ').First().ToLower();
					//Removed input type
					chatMessage = new string(ChatEnterText.Text.Skip(inputType.Length).ToArrayTryAvoidCopy());

					switch (inputType)
					{
						case "/guild":
						case "/g":
							type = ChatChannelType.Guild;
							break;
						case "/say":
						case "/s":
							type = ChatChannelType.Proximity;
							break;
					}
				}

				OnChatMessageEntered?.Invoke(this, new ChatTextMessageEnteredEventArgs(type, chatMessage));

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

			//For reference
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
		}
	}
}
