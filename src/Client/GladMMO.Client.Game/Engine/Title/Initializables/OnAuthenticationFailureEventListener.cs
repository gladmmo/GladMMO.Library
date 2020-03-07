using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class OnAuthenticationFailureEventListener : BaseSingleEventListenerInitializable<IAuthenticationResultRecievedEventSubscribable, AuthenticationResultEventArgs>
	{
		private IUIButton LoginButton { get; }

		private IUIText PasswordInput { get; }

		/// <inheritdoc />
		public OnAuthenticationFailureEventListener(IAuthenticationResultRecievedEventSubscribable subscriptionService, 
			[KeyFilter(UnityUIRegisterationKey.Login)] [NotNull] IUIButton loginButton,
			[KeyFilter(UnityUIRegisterationKey.PasswordTextBox)] [NotNull] IUIText passwordInput)
			: base(subscriptionService)
		{
			LoginButton = loginButton ?? throw new ArgumentNullException(nameof(loginButton));
			PasswordInput = passwordInput ?? throw new ArgumentNullException(nameof(passwordInput));
		}

		protected override void OnEventFired(object source, AuthenticationResultEventArgs args)
		{
			//Only interested in failure.
			if (args.isSuccessful)
				return;

			//Just renable the login button on failure
			//and clear the password as it's likely what is wrong.
			LoginButton.IsInteractable = true;
			PasswordInput.Text = "";
		}
	}
}
