using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GladMMO
{
	[Serializable]
	public sealed class ContentResourceHandle : IPrefabContentResourceHandle
	{
		private string ContentPath { get; }

		private AsyncOperationHandle<GameObject> AssetLoadTask { get; set; }

		public ContentResourceHandle([NotNull] string contentPath)
		{
			ContentPath = contentPath ?? throw new ArgumentNullException(nameof(contentPath));
		}

		/// <inheritdoc />
		public Task<GameObject> LoadPrefabAsync()
		{
			//TODO: Cache and share initial assetbundle details gathered.
			TaskCompletionSource<GameObject> result = new TaskCompletionSource<GameObject>();
			AssetLoadTask = Addressables.LoadAssetAsync<GameObject>(ContentPath);

			AssetLoadTask.Completed += operation =>
			{
				if (operation.Status == AsyncOperationStatus.Failed)
				{
					//TODO: We're assuming success, is that bad?
					result.SetException(new Exception($"Failed to load Content: {ContentPath}. Reason: {operation.OperationException?.Message} OpName: {operation.DebugName}"));
				}
				else
					//TODO: We're assuming success, is that bad?
					result.SetResult(AssetLoadTask.Result);
			};

			return result.Task;
		}
	}
}
