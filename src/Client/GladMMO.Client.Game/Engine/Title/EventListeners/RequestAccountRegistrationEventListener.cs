using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using Refit;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class RequestAccountRegistrationEventListener : ButtonClickedEventListener<IRegisterAccountButtonClickedEventSubscribable>
	{
		/// <summary>
		/// The authentication service.
		/// </summary>
		private IAuthenticationService AuthService { get; }

		/// <summary>
		/// The username text field.
		/// </summary>
		public IUIText UsernameText { get; }

		/// <summary>
		/// The password text field.
		/// </summary>
		public IUIText PasswordText { get; }

		private ILog Logger { get; }

		private IGeneralErrorEncounteredEventPublisher ErrorPublisher { get; }

		public RequestAccountRegistrationEventListener(IRegisterAccountButtonClickedEventSubscribable subscriptionService,
			[NotNull] IAuthenticationService authService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.UsernameTextBox)] IUIText usernameText,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.PasswordTextBox)] IUIText passwordText,
			[NotNull] IGeneralErrorEncounteredEventPublisher errorPublisher,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			AuthService = authService ?? throw new ArgumentNullException(nameof(authService));
			UsernameText = usernameText ?? throw new ArgumentNullException(nameof(usernameText));
			PasswordText = passwordText ?? throw new ArgumentNullException(nameof(passwordText));
			ErrorPublisher = errorPublisher ?? throw new ArgumentNullException(nameof(errorPublisher));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			//Turn off button temporarily
			args.Button.IsInteractable = false;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await AuthService.TryRegister(UsernameText.Text, PasswordText.Text)
						.ConfigureAwait(true);

					//If this didn't fail we should re-enable register, even though they shouldn't really do it again.
					args.Button.IsInteractable = true;
				}
				catch (ApiException apiException)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Encountered Auth Error: {apiException.Message}\n\nStack: {apiException.StackTrace}");

					ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs("Registration Failed", $"Error Code: {(int)apiException.StatusCode} Reason: {apiException.Content}. {apiException.Message}", () => args.Button.IsInteractable = true));
					throw;
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Encountered Auth Error: {e.Message}\n\nStack: {e.StackTrace}");

					ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs("Registration Failed", $"Reason: Unknown Server/Client Error: {e.Message}", () => args.Button.IsInteractable = true));
					throw;
				}
			});
		}
	}
}
