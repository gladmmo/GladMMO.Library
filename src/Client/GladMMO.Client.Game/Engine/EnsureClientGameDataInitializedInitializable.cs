using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO.Engine
{
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class EnsureClientGameDataInitializedInitializable : IGameInitializable
	{
		private IClientDataCollectionContainer DataContainer { get; }

		public EnsureClientGameDataInitializedInitializable([NotNull] IClientDataCollectionContainer dataContainer)
		{
			DataContainer = dataContainer ?? throw new ArgumentNullException(nameof(dataContainer));
		}

		public async Task OnGameInitialized()
		{
			await DataContainer.DataLoadingTask;
		}
	}
}
