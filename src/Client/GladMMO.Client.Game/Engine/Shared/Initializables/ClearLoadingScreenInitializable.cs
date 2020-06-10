using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class ClearLoadingScreenInitializable : IGameInitializable
	{
		private IUIElement LoadingScreenRoot { get; }

		public ClearLoadingScreenInitializable([KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIElement loadingScreenRoot)
		{
			LoadingScreenRoot = loadingScreenRoot ?? throw new ArgumentNullException(nameof(loadingScreenRoot));
		}

		public Task OnGameInitialized()
		{
			if(LoadingScreenRoot.isActive)
				LoadingScreenRoot.SetElementActive(false);

			return Task.CompletedTask;
		}
	}
}
