using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DisableLoadingScreenOnLocalPlayerSpawnedEventListener : OnLocalPlayerSpawnedEventListener
	{
		public IUIElement LoadingScreenRoot { get; }

		public DisableLoadingScreenOnLocalPlayerSpawnedEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIElement loadingScreenRoot) 
			: base(subscriptionService)
		{
			LoadingScreenRoot = loadingScreenRoot ?? throw new ArgumentNullException(nameof(loadingScreenRoot));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			LoadingScreenRoot.SetElementActive(false);
		}
	}
}
