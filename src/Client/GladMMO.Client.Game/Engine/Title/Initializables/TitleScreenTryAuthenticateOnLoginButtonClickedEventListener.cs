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

		/// <inheritdoc />
		public TitleScreenTryAuthenticateOnLoginButtonClickedEventListener(
			[NotNull] ILoginButtonClickedEventSubscribable subscriptionService,
			[NotNull] IAuthenticationService authService,
			[NotNull] ILog logger,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.UsernameTextBox)] IUIText usernameText,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.PasswordTextBox)] IUIText passwordText)
			: base(subscriptionService)
		{
			AuthService = authService ?? throw new ArgumentNullException(nameof(authService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			UsernameText = usernameText ?? throw new ArgumentNullException(nameof(usernameText));
			PasswordText = passwordText ?? throw new ArgumentNullException(nameof(passwordText));
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
				JWTModel jwtModel = null;

				//TODO: Validate username and password
				//We can't do error code supression with refit anymore, so we have to do this crap.
				try
				{
					jwtModel = await AuthService.TryAuthenticate(BuildAuthRequestModel())
						.ConfigureAwait(false);
				}
				catch (ApiException e)
				{
					jwtModel = e.GetContentAs<JWTModel>();

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
						Logger.Debug($"Auth Response for User: {UsernameText.Text} Result: {jwtModel?.isTokenValid} OptionalError: {jwtModel?.Error} OptionalErrorDescription: {jwtModel?.ErrorDescription}");

					//Even if it's null, we should broadcast the event.
					OnAuthenticationResultRecieved?.Invoke(this, new AuthenticationResultEventArgs(jwtModel));
				}
			});
		}
	}
}
