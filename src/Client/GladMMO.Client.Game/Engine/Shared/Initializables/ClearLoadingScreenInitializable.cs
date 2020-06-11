using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class ClearLoadingScreenInitializable : IGameInitializable
	{
		private ILoadingScreenManagementService LoadingScreenService { get; }

		public ClearLoadingScreenInitializable([NotNull] ILoadingScreenManagementService loadingScreenService)
		{
			LoadingScreenService = loadingScreenService ?? throw new ArgumentNullException(nameof(loadingScreenService));
		}

		public Task OnGameInitialized()
		{
			if(LoadingScreenService.isActive)
				LoadingScreenService.Disable();

			return Task.CompletedTask;
		}
	}
}
