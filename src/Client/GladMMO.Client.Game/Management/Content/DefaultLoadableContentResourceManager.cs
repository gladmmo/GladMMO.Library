using System; using FreecraftCore;
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
	public abstract class DefaultLoadableContentResourceManager : ILoadableContentResourceManager, IDisposable
	{
		private ILog Logger { get; }

		public UserContentType ContentType { get; }

		private readonly object SyncObj = new object();

		/// <summary>
		/// Indicates if the entire resource manager has been disposed.
		/// </summary>
		public bool isDisposed { get; private set; } = false;

		/// <inheritdoc />
		protected DefaultLoadableContentResourceManager(
			[NotNull] ILog logger,
			UserContentType contentType)
		{
			if(!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int)contentType, typeof(UserContentType));

			//TODO: We haven't implemented the refcounted cleanup. We ref count, but don't yet dispose.
			ProjectVersionStage.AssertAlpha();

			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ContentType = contentType;
			ReleaseUnmanagedResources();
		}

		/// <inheritdoc />
		public async Task<IPrefabContentResourceHandle> LoadContentPrefabAsync(long contentId)
		{
			ContentDownloadURLResponse downloadUrlResponse = await RequestDownloadURL(contentId);

			//TODO: Handle failure
			TaskCompletionSource<IPrefabContentResourceHandle> completionSource = new TaskCompletionSource<IPrefabContentResourceHandle>();

			//Asset bundle requests can sadly only happen on the main thread, so we must join the main thread.
			await new UnityYieldAwaitable();

			if(!downloadUrlResponse.isSuccessful)
				throw new InvalidOperationException($"Failed to Load: {ContentType} Id: {contentId}");

			//When we first get back on the main thread, the main concern
			//is that this resource manager may be from the last scene
			//and that the client may have moved on
			//to avoid this issues we check disposal state
			//and do nothing, otherwise if we check AFTER then we just have to release the assetbundle immediately anyway.
			if(isDisposed)
			{
				//Just tell anyone awaiting this that it is canceled. They should handle that case, not us.
				completionSource.SetCanceled();
				throw new TaskCanceledException("Content load cancelled.");
			}

			//GetContent will throw if the assetbundle has already been loaded.
			//So to prevent this from occuring due to multiple requests for the
			//content async we will check, on this main thread, via a write lock.
			lock(SyncObj)
			{
				try
				{
					//otherwise, we still don't have it so we should initialize it.
					completionSource.SetResult(new ContentResourceHandle(downloadUrlResponse.DownloadURL)); //we assume this will work now.
				}
				catch(Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to load AssetBundle for Content: {contentId}. Url: {downloadUrlResponse.DownloadURL} Reason: {e.ToString()}");

					//Don't rethrow, causing build breaking.
				}
			}

			return await completionSource.Task
				.ConfigureAwaitFalse();
		}

		protected abstract Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId);

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
				//if(ResourceHandleCache.Count != 0)
					UnloadAllBundles();
		}

		private void UnloadAllBundles()
		{
			/*foreach(var entry in ResourceHandleCache.Values)
				entry.Bundle.Unload(true);*/
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