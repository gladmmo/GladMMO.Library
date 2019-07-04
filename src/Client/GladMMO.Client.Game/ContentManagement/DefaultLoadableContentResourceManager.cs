using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.Networking;

namespace GladMMO
{
	//TODO: We should do some threading and safety stuff.
	public sealed class DefaultLoadableContentResourceManager : ILoadableContentResourceManager, IDisposable
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		private ILog Logger { get; }

		//We should only tocuh this on the main thread, including cleanup and updating it.
		private Dictionary<long, ReferenceCountedPrefabContentResourceHandle> ResourceHandleCache { get; }

		private readonly object SyncObj = new object();

		/// <summary>
		/// Indicates if the entire resource manager has been disposed.
		/// </summary>
		public bool isDisposed { get; private set; } = false;

		/// <inheritdoc />
		public DefaultLoadableContentResourceManager(
			[NotNull] IDownloadableContentServerServiceClient contentClient,
			[NotNull] IReadonlyAuthTokenRepository authTokenRepo,
			[NotNull] ILog logger)
		{
			//TODO: We haven't implemented the refcounted cleanup. We ref count, but don't yet dispose.
			ProjectVersionStage.AssertAlpha();

			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
			AuthTokenRepo = authTokenRepo ?? throw new ArgumentNullException(nameof(authTokenRepo));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			ResourceHandleCache = new Dictionary<long, ReferenceCountedPrefabContentResourceHandle>();

			ReleaseUnmanagedResources();
		}

		/// <inheritdoc />
		public bool IsAvatarResourceAvailable(long avatarId)
		{
			if(avatarId < 0) throw new ArgumentOutOfRangeException(nameof(avatarId));

			lock(SyncObj)
				return ResourceHandleCache.ContainsKey(avatarId);
		}

		/// <inheritdoc />
		public async Task<IPrefabContentResourceHandle> LoadAvatarPrefabAsync(long avatarId)
		{
			//If it's already available, we can just return immediately
			if(IsAvatarResourceAvailable(avatarId))
				return TryLoadAvatarPrefab(avatarId);

			ContentDownloadURLResponse downloadUrlResponse = await ContentClient.RequestAvatarDownloadUrl(avatarId)
				.ConfigureAwait(false);

			//TODO: Handle failure
			TaskCompletionSource<IPrefabContentResourceHandle> completionSource = new TaskCompletionSource<IPrefabContentResourceHandle>();

			//Asset bundle requests can sadly only happen on the main thread, so we must join the main thread.
			await new UnityYieldAwaitable();

			//TODO: We should handle caching, versioning and etc here.
			UnityWebRequestAsyncOperation asyncOperation = UnityWebRequestAssetBundle.GetAssetBundle(downloadUrlResponse.DownloadURL, 0).SendWebRequest();

			//TODO: We should render these operations to the loading screen UI.
			asyncOperation.completed += operation =>
			{
				//When we first get back on the main thread, the main concern
				//is that this resource manager may be from the last scene
				//and that the client may have moved on
				//to avoid this issues we check disposal state
				//and do nothing, otherwise if we check AFTER then we just have to release the assetbundle immediately anyway.
				if(isDisposed)
				{
					//Just tell anyone awaiting this that it is canceled. They should handle that case, not us.
					completionSource.SetCanceled();
					return;
				}


				//GetContent will throw if the assetbundle has already been loaded.
				//So to prevent this from occuring due to multiple requests for the
				//content async we will check, on this main thread, via a write lock.
				lock(SyncObj)
				{
					//We're on the main thread again. So, we should check if another
					//request already got the bundle
					if(IsAvatarResourceAvailable(avatarId))
					{
						completionSource.SetResult(TryLoadAvatarPrefab(avatarId));
						return;
					}

					//otherwise, we still don't have it so we should initialize it.
					this.ResourceHandleCache[avatarId] = new ReferenceCountedPrefabContentResourceHandle(DownloadHandlerAssetBundle.GetContent(asyncOperation.webRequest));
					completionSource.SetResult(TryLoadAvatarPrefab(avatarId)); //we assume this will work now.
				}
			};

			return await completionSource.Task
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public IPrefabContentResourceHandle TryLoadAvatarPrefab(long avatarId)
		{
			lock(SyncObj)
			{
				if(!IsAvatarResourceAvailable(avatarId))
					throw new InvalidOperationException($"Cannot load AvatarId: {avatarId} from memory. Call {nameof(LoadAvatarPrefabAsync)} if not already in memory.");

				//Important to claim reference, since this is ref counted.
				var handle = ResourceHandleCache[avatarId];
				handle.ClaimReference();

				return new SingleReleaseablePrefabContentResourceHandleDecorator(handle);
			}
		}

		private void ReleaseUnmanagedResources()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("Disposing of asset bundles.");

			lock(SyncObj)
				//this isn't really needed, but for VS unit testing it HATES that
				//That the bundle.Unload is an INTERNAL native call
				//for an assembly not packaged with UnityEngine.dll
				//So to avoid issues in VS editor we do this, and it won't try to load it thanks
				//to lazy JIT.
				if(ResourceHandleCache.Count != 0)
					UnloadAllBundles();
		}

		private void UnloadAllBundles()
		{
			foreach(var entry in ResourceHandleCache.Values)
				entry.Bundle.Unload(true);
		}

		/// <inheritdoc />
		public void Dispose()
		{
			ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
			isDisposed = true;
		}

		~DefaultLoadableContentResourceManager()
		{
			if(!isDisposed)
				ReleaseUnmanagedResources();
		}
	}
}