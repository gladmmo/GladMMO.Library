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
		private ILoadingScreenManagementService LoadingScreenService { get; }

		public DisableLoadingScreenOnLocalPlayerSpawnedEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ILoadingScreenManagementService loadingScreenService) 
			: base(subscriptionService)
		{
			LoadingScreenService = loadingScreenService ?? throw new ArgumentNullException(nameof(loadingScreenService));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			LoadingScreenService.Disable();
		}
	}
}
