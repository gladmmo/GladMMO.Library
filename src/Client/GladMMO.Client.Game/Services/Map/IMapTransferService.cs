using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GladMMO
{
	public interface IMapTransferService
	{
		/// <summary>
		/// Async map transfer.
		/// Will move the client from one map to another.
		/// Unloads the current map scene and then loads the new map scene.
		/// </summary>
		/// <param name="mapId">The map id to load.</param>
		/// <returns>Awaitable for map transfer complete or failure.</returns>
		Task TransferToMapAsync(int mapId);
	}

	public sealed class DefaultMapTransferService : IMapTransferService
	{
		private ILoadingScreenManagementService LoadingScreenService { get; }

		private IInstanceSceneChangeRequestedEventPublisher SceneChangePublisher { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private ILog Logger { get; }

		public DefaultMapTransferService([NotNull] ILoadingScreenManagementService loadingScreenService,
			[NotNull] IInstanceSceneChangeRequestedEventPublisher sceneChangePublisher,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] ILog logger)
		{
			LoadingScreenService = loadingScreenService ?? throw new ArgumentNullException(nameof(loadingScreenService));
			SceneChangePublisher = sceneChangePublisher ?? throw new ArgumentNullException(nameof(sceneChangePublisher));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task TransferToMapAsync(int mapId)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Pending Map transfer to MapId: {mapId}");

			LoadingScreenService.EnableLoadingScreenForMap(mapId);

			//Now we're showing the loading screen of the map we will be headed to, we need to stop client message handling
			//so we can prepare to surgically extract to the new map instance.
			//But we cannot do this directly, we just have to do this indirectly by broadcasting a scene change event.
			//TODO: Remove old PSO playable game scene enum
			SceneChangePublisher.PublishEvent(this, new RequestedSceneChangeEventArgs(PlayableGameScene.Episode1Pioneer2, mapId));

			await new UnityYieldAwaitable();

			//Unload all scenes, we need to remove the current instance server scene
			//and the current map scene.
			await GladMMOSceneManager.UnloadAllAddressableScenesAsync();

			/*AsyncOperationHandle worldLoadHandle = GladMMOSceneManager.LoadAddressableSceneAsync(new MapFilePath(map.Directory));
			worldLoadHandle.Completed += op => GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME);*/

			//Assume the map exists
			MapEntry<string> map = ClientData.AssertEntry<MapEntry<string>>(mapId);

			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

			//Now we should load the two maps one on top of eachother.
			try
			{
				//TODO: Once ASYNC Addressable loading doesn't RANDOMLY throw fucking NULL REFs fucking UNITY we don't need this nested disaster.
				GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(new MapFilePath(map.Directory), true)
					.Completed += handle =>
					{
						if(handle.Status != AsyncOperationStatus.Succeeded)
						{
							taskCompletionSource.SetException(new InvalidOperationException($"Failed to load: {map.Directory} for Map Transfer."));
							return;
						}

						GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME)
							.Completed += handle2 =>
							{
								if(handle2.Status == AsyncOperationStatus.Succeeded)
								{
									taskCompletionSource.SetResult(null);
								}
								else
									taskCompletionSource.SetException(new InvalidOperationException($"Failed to load: {GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME} for Map Transfer."));
							};
					};
			}
			catch(Exception e)
			{
				InvalidOperationException exception = new InvalidOperationException($"Failed to load Map: {map.MapId} Directory: {map.Directory}. Reason: {e.Message}", e);
				taskCompletionSource.SetException(exception);
				throw exception;
			}

			await taskCompletionSource.Task;
		}
	}
}
