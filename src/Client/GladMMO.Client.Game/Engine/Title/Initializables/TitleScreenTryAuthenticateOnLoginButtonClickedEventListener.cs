using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using Refit;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IAuthenticationResultRecievedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class TitleScreenTryAuthenticateOnLoginButtonClickedEventListener : BaseSingleEventListenerInitializable<ILoginButtonClickedEventSubscribable>, IAuthenticationResultRecievedEventSubscribable
	{
		/// <summary>
		/// The authentication service.
		/// </summary>
		private IAuthenticationService AuthService { get; }

		/// <summary>
		/// Logger.
		/// </summary>
		private ILog Logger { get; }

		/// <summary>
		/// The username text field.
		/// </summary>
		public IUIText UsernameText { get; }

		/// <summary>
		/// The password text field.
		/// </summary>
		public IUIText PasswordText { get; }

		/// <inheritdoc />
		public event EventHandler<AuthenticationResultEventArgs> OnAuthenticationResultRecieved;

		private IGeneralErrorEncounteredEventPublisher ErrorPublisher { get; }

		/// <inheritdoc />
		public TitleScreenTryAuthenticateOnLoginButtonClickedEventListener(
			[NotNull] ILoginButtonClickedEventSubscribable subscriptionService,
			[NotNull] IAuthenticationService authService,
			[NotNull] ILog logger,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.UsernameTextBox)] IUIText usernameText,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.PasswordTextBox)] IUIText passwordText,
			[NotNull] IGeneralErrorEncounteredEventPublisher errorPublisher)
			: base(subscriptionService)
		{
			AuthService = authService ?? throw new ArgumentNullException(nameof(authService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			UsernameText = usernameText ?? throw new ArgumentNullException(nameof(usernameText));
			PasswordText = passwordText ?? throw new ArgumentNullException(nameof(passwordText));
			ErrorPublisher = errorPublisher ?? throw new ArgumentNullException(nameof(errorPublisher));
		}

		private AuthenticationRequestModel BuildAuthRequestModel()
		{
			return new AuthenticationRequestModel(UsernameText.Text, PasswordText.Text);
		}

		//TODO: Simplified async event firing/handling
		/// <inheritdoc />
		protected override void OnEventFired(object source, EventArgs args)
		{
			//We should not do async OnEventFired because we will get silent failures.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				PlayerAccountJWTModel PlayerAccountJWTModel = null;

				//TODO: Validate username and password
				//We can't do error code supression with refit anymore, so we have to do this crap.
				try
				{
					PlayerAccountJWTModel = await AuthService.TryAuthenticate(BuildAuthRequestModel())
						.ConfigureAwait(false);
				}
				catch (ApiException e)
				{
					PlayerAccountJWTModel = e.GetContentAs<PlayerAccountJWTModel>();

					if (Logger.IsErrorEnabled)
						Logger.Error($"Encountered Auth Error: {e.Message}");
				}
				catch (Exception e)
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Encountered Auth Error: {e.Message}\n\nStack: {e.StackTrace}");
				}
				finally
				{
					if(Logger.IsDebugEnabled)
						Logger.Debug($"Auth Response for User: {UsernameText.Text} Result: {PlayerAccountJWTModel?.isTokenValid} OptionalError: {PlayerAccountJWTModel?.Error} OptionalErrorDescription: {PlayerAccountJWTModel?.ErrorDescription}");

					//TODO: Rename event as authentication success.
					if(PlayerAccountJWTModel != null && PlayerAccountJWTModel.isTokenValid)
						//Even if it's null, we should broadcast the event.
						OnAuthenticationResultRecieved?.Invoke(this, new AuthenticationResultEventArgs(PlayerAccountJWTModel));
					else if (PlayerAccountJWTModel != null)
					{
						//Non-null response but failed.
						ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs("Login Failed", $"Reason: {PlayerAccountJWTModel.Error}. {PlayerAccountJWTModel.ErrorDescription}", null));
					}
					else
					{
						//failed and null response.
						//Non-null response but failed.
						ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs("Login Failed", $"Reason: Unknown Server Error", null));
					}
				}
			});
		}
	}
}
