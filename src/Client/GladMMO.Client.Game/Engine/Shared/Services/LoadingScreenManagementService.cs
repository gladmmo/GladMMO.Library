using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GladMMO
{
	public interface ILoadingScreenManagementService
	{
		/// <summary>
		/// Indicates if the loading screen is active.
		/// </summary>
		bool isActive { get; }

		/// <summary>
		/// Sets the active state of the loading screen to false/off.
		/// Affects <see cref="isActive"/>.
		/// Broadcasts an event via <see cref="ILoadingScreenStateChangedEventSubscribable"/>
		/// </summary>
		void Disable();

		/// <summary>
		/// Sets the loading screen to use the loadingscreen
		/// associated with the map with MapId.
		/// </summary>
		/// <param name="mapId">The id of the map.</param>
		void EnableLoadingScreenForMap(int mapId);

		/// <summary>
		/// Registers an operation for loadingscreen bar.
		/// </summary>
		/// <param name="operation">The operations.</param>
		void RegisterOperation(AsyncOperationHandle operation);
	}

	public sealed class LoadingScreenOperationContainer
	{
		//Needs to be static so it can be shared across instances.
		public static Stack<AsyncOperationHandle> OperationStack { get; } = new Stack<AsyncOperationHandle>();
	}

	[AdditionalRegisterationAs(typeof(ILoadingScreenStateChangedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(ILoadingScreenManagementService))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class LoadingScreenManagementService : ILoadingScreenManagementService, IGameInitializable, ILoadingScreenStateChangedEventSubscribable
	{
		public bool isActive => LoadingScreenRoot.isActive;

		public event EventHandler<LoadingScreenStateChangedEventArgs> OnLoadingScreenStateChanged;

		public IClientDataCollectionContainer ClientData { get; }

		private ILog Logger { get; }

		private IUIElement LoadingScreenRoot { get; }

		public IUIImage LoadingScreenBackgroundImage { get; }

		private IUIFillableImage LoadingScreenFillBar { get; }

		//Prevent loading screen races because they may set maps inbetween loading the texture.
		private AsyncLock SyncObj { get; } = new AsyncLock();

		public LoadingScreenManagementService([NotNull] IClientDataCollectionContainer clientData, 
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIElement loadingScreenRoot,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIImage loadingScreenBackgroundImage,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreenBar)] IUIFillableImage loadingScreenFillBar)
		{
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			LoadingScreenRoot = loadingScreenRoot ?? throw new ArgumentNullException(nameof(loadingScreenRoot));
			LoadingScreenBackgroundImage = loadingScreenBackgroundImage ?? throw new ArgumentNullException(nameof(loadingScreenBackgroundImage));
			LoadingScreenFillBar = loadingScreenFillBar;
		}

		public void Disable()
		{
			InternalChangeState(false);
		}

		private void InternalChangeState(bool state)
		{
			//Can only do this on the main thread.
			if (UnityAsyncHelper.UnityMainThreadContext == SynchronizationContext.Current)
				InternalSetActive(state);
			else
			{
				//Not on main thread
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
				{
					using (await SyncObj.LockAsync())
					{
						InternalSetActive(state);
					}
				});
			}
		}

		private void InternalSetActive(bool state)
		{
			if (state == isActive)
				return;

			LoadingScreenRoot.SetElementActive(state);
			OnLoadingScreenStateChanged?.Invoke(this, new LoadingScreenStateChangedEventArgs(state));
		}

		public void EnableLoadingScreenForMap(int mapId)
		{
			if (!ClientData.HasEntry<MapEntry<string>>(mapId))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Requested LoadingScreen for Map: {mapId} but doesn't exist.");

				//Even if we don't have it, load whatever we DO have as a default.
				InternalChangeState(true);
				return;
			}

			int loadingScreenId = ClientData.AssertEntry<MapEntry<string>>(mapId).LoadingScreenId;

			if (!ClientData.HasEntry<LoadingScreensEntry<string>>(loadingScreenId))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Requested LoadingScreen for Map: {mapId} with LoadingScreen: {loadingScreenId} but doesn't exist.");

				//Even if we don't have it, load whatever we DO have as a default.
				InternalChangeState(true);
				return;
			}

			LoadingScreensEntry<string> loadingScreenEntry = ClientData.AssertEntry<LoadingScreensEntry<string>>(loadingScreenId);

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				using (await SyncObj.LockAsync())
				{
					//TODO: This FUCKING shitty Addressable system is SO unfinished. It DOES NOT support task awaiting yet.
					string currentPath = $"Assets/Content/{loadingScreenEntry.FilePath.Replace(".blp", ".png")}".Replace('\\', '/');

					/*Texture2D texture = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Texture2D>(currentPath).Task;
					LoadingScreenBackgroundImage.SetSpriteTexture(texture);*/
					UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Texture2D>(currentPath).Completed += handle =>
					{
						try
						{
							if (handle.Status == AsyncOperationStatus.Succeeded)
								LoadingScreenBackgroundImage.SetSpriteTexture(handle.Result);
							else if (Logger.IsErrorEnabled)
								Logger.Error($"Failed to load LoadingScreen: {currentPath}");
						}
						finally
						{
							InternalSetActive(true);
						}
					};
				}
			});
		}

		public void RegisterOperation(AsyncOperationHandle operation)
		{
			LoadingScreenOperationContainer.OperationStack.Push(operation);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}

		//Nonsesical hack from: https://forum.unity.com/threads/0-7-4-addressables-downloaddependencies-percentcomplete-always-returns-0.667558/
		//Results aren't real, but feel real-ish.
		private static float CalculatePercentComplete(AsyncOperationHandle asyncOperation)
		{
			var deps = new List<AsyncOperationHandle>();
			asyncOperation.GetDependencies(deps); // deps is added to! (weird API...)
			float percentCompleteSum = 0;
			foreach(var asyncOperationHandle in deps)
			{
				percentCompleteSum += asyncOperationHandle.PercentComplete;
			}
			return percentCompleteSum / deps.Count;
		}
	}
}
