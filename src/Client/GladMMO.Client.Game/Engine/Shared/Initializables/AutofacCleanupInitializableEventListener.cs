using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class AutofacCleanupInitializableEventListener : IGameInitializable
	{
		private ILifetimeScope AutofacScope { get; }

		public AutofacCleanupInitializableEventListener([NotNull] ILifetimeScope autofacScope)
		{
			AutofacScope = autofacScope ?? throw new ArgumentNullException(nameof(autofacScope));
		}

		public Task OnGameInitialized()
		{
			SceneManager.sceneLoaded += CleanupAutofac;
			return Task.CompletedTask;
		}

		public void CleanupAutofac(Scene scene, LoadSceneMode mode)
		{
			AutofacScope.Dispose();
			SceneManager.sceneLoaded -= CleanupAutofac;
		}
	}
}
