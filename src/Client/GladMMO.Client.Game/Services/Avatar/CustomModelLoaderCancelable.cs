using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	public sealed class CustomModelLoaderCancelable : IDisposable
	{
		private Task<IPrefabContentResourceHandle> PrefabHandleFuture { get; }

		private ILog Logger { get; }

		private Action<GameObject> OnAvatarPrefabReadyCallback { get; }

		/// <summary>
		/// Mutable cancellation state.
		/// </summary>
		private CancellationTokenSource CurrentCancellationSource { get; set; }

		public CustomModelLoaderCancelable([NotNull] Task<IPrefabContentResourceHandle> prefabHandleFuture, [NotNull] ILog logger, [NotNull] Action<GameObject> avatarPrefabReadyCallback)
		{
			PrefabHandleFuture = prefabHandleFuture ?? throw new ArgumentNullException(nameof(prefabHandleFuture));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			OnAvatarPrefabReadyCallback = avatarPrefabReadyCallback ?? throw new ArgumentNullException(nameof(avatarPrefabReadyCallback));
		}

		public void StartLoading()
		{
			//We NEVER want to directly reference the cancel token in an async context due to race conditions
			//we should only ref it from this local ref.
			//Local ref to the cancellation token source
			CurrentCancellationSource = new CancellationTokenSource();

			//TODO: Asset multiple calls
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				//This guards and prevents unwanted and wasted resources into
				//loading an avatar that has had its token canceled.
				if(IsModelChangeCanceled(CurrentCancellationSource))
				{
					ModelChangeCancelLogging();
					return;
				}

				//We need to await the resource but capture the context, because we'll need to be on the main thread.
				IPrefabContentResourceHandle handle = await PrefabHandleFuture
					.ConfigureAwait(true);

				//This guards and prevents unwanted and wasted resources into
				//loading an avatar that has had its token canceled.
				if(IsModelChangeCanceled(CurrentCancellationSource))
				{
					ModelChangeCancelLogging();
					return;
				}

				GameObject gameObject = await handle.LoadPrefabAsync()
					.ConfigureAwait(true);

				//This guards and prevents unwanted and wasted resources into
				//loading an avatar that has had its token canceled.
				if(IsModelChangeCanceled(CurrentCancellationSource))
				{
					ModelChangeCancelLogging();
					return;
				}

				OnAvatarPrefabReadyCallback.Invoke(gameObject);
			});
		}

		private void CancelCurrentCancellationToken()
		{
			CurrentCancellationSource?.Cancel();
			//Dispose just causes us issues, only LINKED tokens require disposal.
			//see: https://stackoverflow.com/questions/6960520/when-to-dispose-cancellationtokensource
		}

		private void ModelChangeCancelLogging()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Encountered canceled request for Entity: {"UNKNOWN"} to change to ModelId: {"UNKNOWN"}");
		}

		private static bool IsModelChangeCanceled(CancellationTokenSource cancelToken)
		{
			return cancelToken.Token.IsCancellationRequested;
		}

		public void Cancel()
		{
			CancelCurrentCancellationToken();
		}

		public void Dispose()
		{
			Cancel();
		}
	}
}
