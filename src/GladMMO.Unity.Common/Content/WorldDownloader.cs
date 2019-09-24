using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public sealed class WorldDownloader
	{
		private ILog Logger { get; }

		public WorldDownloader([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> DownloadAsync(string worldDownloadUrl, bool loadSceneAdditive = false)
		{
			TaskCompletionSource<bool> resultSource = new TaskCompletionSource<bool>();

			long worldId = 0;
			try
			{
				//TODO: Do we need to be on the main unity3d thread
				UnityWebRequestAsyncOperation asyncOperation = UnityWebRequestAssetBundle.GetAssetBundle(worldDownloadUrl, 0).SendWebRequest();

				//TODO: We should render these operations to the loading screen UI.
				asyncOperation.completed += async operation =>
				{
					AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(asyncOperation.webRequest);

					string[] paths = bundle.GetAllScenePaths();

					foreach(string p in paths)
						Debug.Log($"Found Scene in Bundle: {p}");

					string scenePath = paths.First();

					await LoadDownloadedScene(scenePath, bundle, loadSceneAdditive);
					resultSource.SetResult(true);
				};
			}
			catch(Exception e)
			{
				/*if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to query for Download URL for ZoneId: {args.ZoneIdentifier} WorldId: {worldId} (0 if never succeeded request). Error: {e.Message}");*/

				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to Download URL: {worldDownloadUrl}. Error: {e.Message}");

				throw;
			}

			return await resultSource.Task;
		}

		private static Task<bool> LoadDownloadedScene(string scenePath, AssetBundle bundle, bool loadSceneAdditive)
		{
			TaskCompletionSource<bool> resultSource = new TaskCompletionSource<bool>();

			//The idea here is that AFTER the scene has been loaded, we need to then load the ACTUAL
			//networking-based gameplay scene. Containing sceneject and all that other stuff.
			//It's basically a layered scene-based approach. Layer 1 is the downloaded scene, layer 2 is the actual nuts and bolts
			//that drive the game.
			SceneManager.sceneLoaded += OnDownloadedSceneFinishedLoading;

			//After this upcoming scene is completed
			AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(System.IO.Path.GetFileNameWithoutExtension(scenePath), loadSceneAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);

			sceneAsync.completed += operation1 =>
			{
				//When the scene is finished loading we should cleanup the asset bundle
				//Don't clean up the WHOLE BUNDLE, just the compressed downloaded data
				bundle.Unload(false);

				//TODO: We need a way/system to reference the bundle later so it can be cleaned up inbetween scene loads.
				resultSource.SetResult(true);
			};

			sceneAsync.allowSceneActivation = true;

			return resultSource.Task;
		}

		private static void OnDownloadedSceneFinishedLoading(Scene sceneLoaded, LoadSceneMode mode)
		{
			//TODO: For zoneserver this is VERY likely to break at some point.
			//Cheapest thing to check first
			if(mode == LoadSceneMode.Single)
			{
				AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME, LoadSceneMode.Additive);
				loadSceneAsync.allowSceneActivation = true;
				loadSceneAsync.completed += o => Debug.Log($"Loaded {GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME}");
			}

			//Unregister this event.
			SceneManager.sceneLoaded -= OnDownloadedSceneFinishedLoading;
		}
	}
}
