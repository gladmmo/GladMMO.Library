using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class AutofacCleanupInitializableEventListener : IGameInitializable
	{
		private ILifetimeScope AutofacScope { get; }

		public AutofacCleanupInitializableEventListener([NotNull] ILifetimeScope autofacScope)
		{
			AutofacScope = autofacScope ?? throw new ArgumentNullException(nameof(autofacScope));
		}

		public async Task OnGameInitialized()
		{
			SceneManager.sceneLoaded += CleanupAutofac;
		}

		public void CleanupAutofac(Scene scene, LoadSceneMode mode)
		{
			AutofacScope.Dispose();
			SceneManager.sceneLoaded -= CleanupAutofac;
		}
	}
}
