using System;
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
	public sealed class TitleScreenOnAuthenticationResultSuccessfulEventListener : BaseSingleEventListenerInitializable<IAuthenticationResultRecievedEventSubscribable, AuthenticationResultEventArgs>
	{
		//TODO: Don't expose Unity directors directly.
		private IUIPlayable SceneEndPlayable { get; }

		public IUIPlayable LoginBoxFadePlayable { get; }

		private IAuthTokenRepository TokenRepository { get; }

		/// <inheritdoc />
		public TitleScreenOnAuthenticationResultSuccessfulEventListener(IAuthenticationResultRecievedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.Login)] [NotNull] IUIPlayable sceneEndPlayable,
			[KeyFilter(UnityUIRegisterationKey.LoginTextBox)] [NotNull] IUIPlayable loginBoxFadePlayable,
			[NotNull] IAuthTokenRepository tokenRepository) 
			: base(subscriptionService)
		{
			SceneEndPlayable = sceneEndPlayable ?? throw new ArgumentNullException(nameof(sceneEndPlayable));
			LoginBoxFadePlayable = loginBoxFadePlayable ?? throw new ArgumentNullException(nameof(loginBoxFadePlayable));
			TokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, [NotNull] AuthenticationResultEventArgs args)
		{
			if(args == null) throw new ArgumentNullException(nameof(args));

			if(!args.isSuccessful)
				return;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				SceneEndPlayable.Play();
				LoginBoxFadePlayable.Play();

				//Init the token repo, otherwise we can't do authorized
				//requests later at all
				TokenRepository.Update(args.TokenResult.AccessToken);

				//TODO: Use the scene manager service.
				AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(1);
				loadSceneAsync.allowSceneActivation = false;

				await Task.Delay(4500);

				loadSceneAsync.allowSceneActivation = true;
			});
		}
	}
}
