using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	public abstract class DefaultLoadableContentResourceManager : ILoadableContentResourceManager, IDisposable
	{
		private ILog Logger { get; }

		public UserContentType ContentType { get; }

		//We should only tocuh this on the main thread, including cleanup and updating it.
		private Dictionary<long, ReferenceCountedPrefabContentResourceHandle> ResourceHandleCache { get; }

		private readonly object SyncObj = new object();

		/// <summary>
		/// Indicates if the entire resource manager has been disposed.
		/// </summary>
		public bool isDisposed { get; private set; } = false;

		/// <inheritdoc />
		public DefaultLoadableContentResourceManager(
			[NotNull] ILog logger,
			UserContentType contentType)
		{
			if(!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int)contentType, typeof(UserContentType));

			//TODO: We haven't implemented the refcounted cleanup. We ref count, but don't yet dispose.
			ProjectVersionStage.AssertAlpha();

			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ContentType = contentType;

			ResourceHandleCache = new Dictionary<long, ReferenceCountedPrefabContentResourceHandle>();

			ReleaseUnmanagedResources();
		}

		/// <inheritdoc />
		public bool IsContentResourceAvailable(long contentId)
		{
			if(contentId < 0) throw new ArgumentOutOfRangeException(nameof(contentId));

			lock(SyncObj)
				return ResourceHandleCache.ContainsKey(contentId);
		}

		/// <inheritdoc />
		public async Task<IPrefabContentResourceHandle> LoadContentPrefabAsync(long contentId)
		{
			//If it's already available, we can just return immediately
			if(IsContentResourceAvailable(contentId))
				return TryLoadContentPrefab(contentId);

			ContentDownloadURLResponse downloadUrlResponse = await RequestDownloadURL(contentId);

			//TODO: Handle failure
			TaskCompletionSource<IPrefabContentResourceHandle> completionSource = new TaskCompletionSource<IPrefabContentResourceHandle>();

			//Asset bundle requests can sadly only happen on the main thread, so we must join the main thread.
			await new UnityYieldAwaitable();

			//TODO: We should handle caching, versioning and etc here.
			UnityWebRequestAsyncOperation asyncOperation = UnityWebRequestAssetBundle.GetAssetBundle(downloadUrlResponse.DownloadURL, (uint)downloadUrlResponse.Version, 0).SendWebRequest();

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
					if(IsContentResourceAvailable(contentId))
					{
						completionSource.SetResult(TryLoadContentPrefab(contentId));
						return;
					}

					//otherwise, we still don't have it so we should initialize it.
					this.ResourceHandleCache[contentId] = new ReferenceCountedPrefabContentResourceHandle(DownloadHandlerAssetBundle.GetContent(asyncOperation.webRequest));
					completionSource.SetResult(TryLoadContentPrefab(contentId)); //we assume this will work now.
				}
			};

			return await completionSource.Task
				.ConfigureAwait(false);
		}

		protected abstract Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId);

		/// <inheritdoc />
		public IPrefabContentResourceHandle TryLoadContentPrefab(long contentId)
		{
			lock(SyncObj)
			{
				if(!IsContentResourceAvailable(contentId))
					throw new InvalidOperationException($"Cannot load contentId: {contentId} from memory. Call {nameof(LoadContentPrefabAsync)} if not already in memory.");

				//Important to claim reference, since this is ref counted.
				var handle = ResourceHandleCache[contentId];
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