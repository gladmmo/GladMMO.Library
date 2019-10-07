using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ICharacterCreationAttemptedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class RequestCharacterCreationEventListener : ButtonClickedEventListener<ICharacterCreationButtonClickedEventSubscribable>, ICharacterCreationAttemptedEventSubscribable
	{
		private ICharacterService CharacterService { get; }

		private IUIText CharacterNameInput { get; }

		private ILog Logger { get; }

		public event EventHandler<CharacterCreationAttemptedEventArgs> OnCharacterCreationAttempted;

		public RequestCharacterCreationEventListener(ICharacterCreationButtonClickedEventSubscribable subscriptionService,
			[NotNull] ICharacterService characterService,
			[KeyFilter(UnityUIRegisterationKey.CharacterNameInput)] [NotNull] IUIText characterNameInput,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
			CharacterNameInput = characterNameInput ?? throw new ArgumentNullException(nameof(characterNameInput));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			string name = CharacterNameInput.Text;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				CharacterCreationResponseCode responseCode = CharacterCreationResponseCode.Success;

				try
				{
					responseCode = await RequestCharacterCreation(name);
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to handle character creation request. Reason: {e.Message}");

					//For the finally block we need to set error
					responseCode = CharacterCreationResponseCode.GeneralServerError;

					throw;
				}
				finally
				{
					//Now, we dispatch an event for the result on the main thread.
					await new UnityYieldAwaitable();

					OnCharacterCreationAttempted?.Invoke(this, new CharacterCreationAttemptedEventArgs(name, responseCode));
				}
			});
		}

		private async Task<CharacterCreationResponseCode> RequestCharacterCreation(string name)
		{
			CharacterNameValidationResponse nameValidationResponse = await CharacterService.ValidateName(name);

			CharacterCreationResponseCode responseCode = CharacterCreationResponseCode.Success;

			if (!nameValidationResponse.isSuccessful)
			{
				if (Logger.IsInfoEnabled)
					Logger.Info($"Failed to validate name. Reason: {nameValidationResponse.ResultCode}");

				responseCode = CharacterCreationResponseCode.NameUnavailableError;
			}
			else
			{
				//If it's valid, we can create it.
				CharacterCreationResponse creationResponse = await CharacterService.CreateCharacter(name);

				if (!creationResponse.isSuccessful)
					if (Logger.IsInfoEnabled)
						Logger.Info($"Failed to create character. Reason: {creationResponse.ResultCode}");

				responseCode = creationResponse.ResultCode;
			}

			return responseCode;
		}
	}
}
