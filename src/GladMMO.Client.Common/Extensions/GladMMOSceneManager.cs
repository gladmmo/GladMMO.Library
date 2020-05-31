using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public static class GladMMOSceneManager
	{
		//Hacky, but don't want to deal with addressables any other way right now
		private static ConcurrentDictionary<string, AsyncOperationHandle<SceneInstance>> AddressableSceneMap { get; } = new ConcurrentDictionary<string, AsyncOperationHandle<SceneInstance>>();

		public static AsyncOperationHandle LoadAddressableSceneAsync([NotNull] string sceneName)
		{
			//Case where our active scene is an addressable scene.
			if (AddressableSceneMap.ContainsKey(SceneManager.GetActiveScene().name))
			{
				AsyncOperationHandle<SceneInstance> sceneHandle = AddressableSceneMap[SceneManager.GetActiveScene().name];
				AddressableSceneMap.TryRemove(SceneManager.GetActiveScene().name, out var _);
				AsyncOperationHandle<SceneInstance> unloadOperationHandle = UnityEngine.AddressableAssets.Addressables.UnloadSceneAsync(sceneHandle);

				AsyncOperationHandle<SceneInstance> sceneLoadHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive, false);
				AddressableSceneMap[sceneName] = sceneLoadHandle;

				unloadOperationHandle.Completed += op =>
				{
					sceneLoadHandle.Completed += loadOperation => ActivateScene(sceneLoadHandle);
				};

				return sceneLoadHandle;
			}
			else
			{
				AsyncOperation unloadOperationHandle = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
				unloadOperationHandle.allowSceneActivation = false;

				AsyncOperationHandle<SceneInstance> sceneLoadHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, false);
				AddressableSceneMap[sceneName] = sceneLoadHandle;

				unloadOperationHandle.completed += op =>
				{
					sceneLoadHandle.Completed += loadOperation => ActivateScene(sceneLoadHandle);
				};
				unloadOperationHandle.allowSceneActivation = true;

				return sceneLoadHandle;
			}
		}

		private static void ActivateScene(AsyncOperationHandle<SceneInstance> sceneLoadHandle)
		{
			sceneLoadHandle.Result.Activate();
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		//We have to set this here because when Activate is called the scene is not instantly loaded.
		private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.name));
		}
	}
}
