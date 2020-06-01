using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public static Task UnloadAllAddressableScenesAsync()
		{
			Task<SceneInstance>[] awaitableUnloads = AddressableSceneMap
				.Values
				.Select(s => UnityEngine.AddressableAssets.Addressables.UnloadSceneAsync(s).Task)
				.ToArray();

			return Task.WhenAll(awaitableUnloads);
		}

		public static AsyncOperationHandle LoadAddressableSceneAdditiveAsync([NotNull] string sceneName)
		{
			AsyncOperationHandle<SceneInstance> sceneLoadHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive, true);

			AddressableSceneMap[sceneName] = sceneLoadHandle;
			return sceneLoadHandle;
		}

		public static AsyncOperationHandle LoadAddressableSceneAsync([NotNull] string sceneName)
		{
			//Case where our active scene is an addressable scene.
			if (AddressableSceneMap.ContainsKey(SceneManager.GetActiveScene().name))
			{
				AddressableSceneMap.TryRemove(SceneManager.GetActiveScene().name, out var sceneHandle);

				if(!sceneHandle.IsValid())
					throw new InvalidOperationException($"Failed to access loaded Addressable Scene for unloading: {SceneManager.GetActiveScene().name}");

				AsyncOperationHandle<SceneInstance> unloadOperationHandle = UnityEngine.AddressableAssets.Addressables.UnloadSceneAsync(sceneHandle);

				AsyncOperationHandle<SceneInstance> sceneLoadHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive, false);

				if (!sceneLoadHandle.IsValid())
					throw new InvalidOperationException($"Failed to load Addressable Scene: {sceneName}");

				AddressableSceneMap[sceneName] = sceneLoadHandle;

				unloadOperationHandle.Completed += op =>
				{
					if(!sceneLoadHandle.IsValid())
						throw new InvalidOperationException($"Failed to setup callback for Addressable Scene: {sceneName}");

					sceneLoadHandle.Completed += loadOperation => ActivateScene(sceneLoadHandle);
				};

				return sceneLoadHandle;
			}
			else
			{
				AsyncOperation unloadOperationHandle = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
				unloadOperationHandle.allowSceneActivation = false;

				AsyncOperationHandle<SceneInstance> sceneLoadHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, false);

				if(!sceneLoadHandle.IsValid())
					throw new InvalidOperationException($"Failed to load Addressable Scene: {sceneName}");

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
			AsyncOperation sceneActivation = sceneLoadHandle.Result.ActivateAsync();
			sceneActivation.completed += o => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadHandle.Result.Scene.name));
		}
	}
}
