using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public static class GladMMOSceneManager
	{
		public static void LoadSceneAsync([NotNull] string sceneName)
		{
			if (string.IsNullOrWhiteSpace(sceneName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(sceneName));

			AsyncOperation unloadActiveSceneAsync = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
			unloadActiveSceneAsync.allowSceneActivation = false;

			AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			loadSceneAsync.allowSceneActivation = false;

			//It's important that the newly loaded scene is set as active, or it messes everything up.
			loadSceneAsync.completed += operation => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

			unloadActiveSceneAsync.completed += operation => loadSceneAsync.allowSceneActivation = true;
			unloadActiveSceneAsync.allowSceneActivation = true;
		}
	}
}
