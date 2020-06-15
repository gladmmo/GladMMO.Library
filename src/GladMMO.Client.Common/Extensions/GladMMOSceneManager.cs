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

		public static async Task UnloadAllAddressableScenesAsync()
		{
			foreach(var scene in AddressableSceneMap.Values)
				Debug.Log($"Unloading Scene: {scene.Result.Scene.name}");

			Task<SceneInstance>[] awaitableUnloads = AddressableSceneMap
				.Values
				.Select(s => UnityEngine.AddressableAssets.Addressables.UnloadSceneAsync(s).Task)
				.ToArray();

			await Task.WhenAll(awaitableUnloads);
			AddressableSceneMap.Clear();
		}

		public static AsyncOperationHandle LoadAddressableSceneAdditiveAsync([NotNull] string sceneName, bool setAsActiveScene = false)
		{
			AsyncOperationHandle<SceneInstance> sceneLoadHandle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive, true);

			AddressableSceneMap[sceneName] = sceneLoadHandle;

			if (setAsActiveScene)
				SetActiveOnComplete(sceneLoadHandle);

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

				AsyncOperationHandle<SceneInstance> sceneLoadHandle = InternalLoadAddressableSceneAsync(sceneName);

				if (!sceneLoadHandle.IsValid() || sceneLoadHandle.Status == AsyncOperationStatus.Failed)
				{
					UnityEngine.AddressableAssets.Addressables.Release(sceneLoadHandle);
					throw new InvalidOperationException($"Failed to load Addressable Scene: {sceneName}");
				}

				AddressableSceneMap[sceneName] = sceneLoadHandle;

				unloadOperationHandle.Completed += op =>
				{
					if(!sceneLoadHandle.IsValid())
						throw new InvalidOperationException($"Failed to setup callback for Addressable Scene: {sceneName}");

					sceneLoadHandle.Completed += loadOperation => SetActiveOnComplete(sceneLoadHandle);
				};

				return sceneLoadHandle;
			}
			else
			{
				AsyncOperation unloadOperationHandle = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
				unloadOperationHandle.allowSceneActivation = false;

				AsyncOperationHandle<SceneInstance> sceneLoadHandle = InternalLoadAddressableSceneAsync(sceneName);

				if (!sceneLoadHandle.IsValid() || sceneLoadHandle.Status == AsyncOperationStatus.Failed)
				{
					UnityEngine.AddressableAssets.Addressables.Release(sceneLoadHandle);
					throw new InvalidOperationException($"Failed to load Addressable Scene: {sceneName}");
				}

				AddressableSceneMap[sceneName] = sceneLoadHandle;

				unloadOperationHandle.completed += op =>
				{
					sceneLoadHandle.Completed += loadOperation => SetActiveOnComplete(sceneLoadHandle);
				};
				unloadOperationHandle.allowSceneActivation = true;

				return sceneLoadHandle;
			}
		}

		private static AsyncOperationHandle<SceneInstance> InternalLoadAddressableSceneAsync(string sceneName)
		{
			try
			{
				return UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive, false);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Attempted to load Scene: {sceneName} but it failed. Reason: {e.Message}", e);
			}
		}

		private static void SetActiveOnComplete(AsyncOperationHandle<SceneInstance> sceneLoadHandle)
		{
			sceneLoadHandle.Completed += op =>
			{
				op.Result.ActivateAsync().completed += op2 =>
				{
					SceneManager.SetActiveScene(op.Result.Scene);

					// Force Unity to asynchronously regenerate the tetrahedral tesselation for all loaded Scenes
					LightProbes.TetrahedralizeAsync();
				};
			};
		}
	}
}
