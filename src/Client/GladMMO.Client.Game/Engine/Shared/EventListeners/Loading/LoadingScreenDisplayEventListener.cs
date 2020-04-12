using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class LoadingScreenDisplayEventListener : BaseSingleEventListenerInitializable<IRequestedSceneChangeEventSubscribable, RequestedSceneChangeEventArgs>
	{
		public IUIElement LoadingScreenRoot { get; }

		public IUIImage LoadingScreenBackgroundImage { get; }

		public LoadingScreenDisplayEventListener([NotNull] IRequestedSceneChangeEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIElement loadingScreenRoot,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIImage loadingScreenBackgroundImage) 
			: base(subscriptionService)
		{
			LoadingScreenRoot = loadingScreenRoot ?? throw new ArgumentNullException(nameof(loadingScreenRoot));
			LoadingScreenBackgroundImage = loadingScreenBackgroundImage ?? throw new ArgumentNullException(nameof(loadingScreenBackgroundImage));
		}

		protected override void OnEventFired(object source, RequestedSceneChangeEventArgs args)
		{
			LoadingScreenRoot.SetElementActive(true);
		}
	}
}
