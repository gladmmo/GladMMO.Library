using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

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

		//Prevent loading screen races because they may set maps inbetween loading the texture.
		private AsyncLock SyncObj { get; } = new AsyncLock();

		public LoadingScreenManagementService([NotNull] IClientDataCollectionContainer clientData, 
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIElement loadingScreenRoot,
			[KeyFilter(UnityUIRegisterationKey.LoadingScreen)] [NotNull] IUIImage loadingScreenBackgroundImage)
		{
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			LoadingScreenRoot = loadingScreenRoot ?? throw new ArgumentNullException(nameof(loadingScreenRoot));
			LoadingScreenBackgroundImage = loadingScreenBackgroundImage ?? throw new ArgumentNullException(nameof(loadingScreenBackgroundImage));
		}

		public void Disable()
		{
			//Can only do this on the main thread.
			if (UnityAsyncHelper.UnityMainThreadContext == SynchronizationContext.Current)
				InternalSetActive(false);
			else
			{
				//Not on main thread
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
				{
					using (await SyncObj.LockAsync())
					{
						InternalSetActive(false);
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
				InternalSetActive(true);
				return;
			}

			int loadingScreenId = ClientData.AssertEntry<MapEntry<string>>(mapId).LoadingScreenId;

			if (!ClientData.HasEntry<LoadingScreensEntry<string>>(loadingScreenId))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Requested LoadingScreen for Map: {mapId} with LoadingScreen: {loadingScreenId} but doesn't exist.");

				//Even if we don't have it, load whatever we DO have as a default.
				InternalSetActive(true);
				return;
			}

			LoadingScreensEntry<string> loadingScreenEntry = ClientData.AssertEntry<LoadingScreensEntry<string>>(loadingScreenId);

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				using (await SyncObj.LockAsync())
				{
					//TODO: Fix addressable path
					try
					{
						string currentPath = $"Assets/Content/{loadingScreenEntry.FilePath.Replace(".blp", ".png")}".Replace('\\', '/');

						Texture2D texture = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Texture2D>(currentPath).Task;
						LoadingScreenBackgroundImage.SetSpriteTexture(texture);
					}
					finally
					{
						InternalSetActive(true);
					}
				}
			});
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
