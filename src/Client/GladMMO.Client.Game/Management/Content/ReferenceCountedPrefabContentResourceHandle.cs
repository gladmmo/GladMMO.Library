using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	[Serializable]
	public sealed class ReferenceCountedPrefabContentResourceHandle : IPrefabContentResourceHandle
	{
		private int _currentUseCount = 0;

		/// <inheritdoc />
		public int CurrentUseCount => _currentUseCount;

		/// <summary>
		/// Indicates if the handle is not referenced/claimed by anyone.
		/// </summary>
		public bool isResourceFreeable => _currentUseCount == 0;

		/// <summary>
		/// The name of the asset bundle.
		/// </summary>
		public string BundleName => Bundle.name;

		public AssetBundle Bundle { get; }

		/// <inheritdoc />
		public ReferenceCountedPrefabContentResourceHandle([NotNull] AssetBundle bundle)
		{
			Bundle = bundle ?? throw new ArgumentNullException(nameof(bundle));
		}

		/// <inheritdoc />
		public void Release()
		{
			Interlocked.Decrement(ref _currentUseCount);
		}

		public void ClaimReference()
		{
			Interlocked.Increment(ref _currentUseCount);
		}

		/// <inheritdoc />
		public GameObject LoadPrefab()
		{
			//TODO: Cache and share initial assetbundle details gathered.
			//TODO: This needs to be refactored.
			string path = GetPrefabPath();

			return Bundle.LoadAsset<GameObject>(path);
		}

		private string GetPrefabPath()
		{
			string[] paths = this.Bundle.GetAllAssetNames();

			foreach(string p in paths)
				Debug.Log($"Found Asset in Bundle: {p}");
			return paths[0];
		}

		/// <inheritdoc />
		public Task<GameObject> LoadPrefabAsync()
		{
			//TODO: Cache and share initial assetbundle details gathered.
			TaskCompletionSource<GameObject> result = new TaskCompletionSource<GameObject>();

			//TODO: Cache and share initial assetbundle details gathered.
			//TODO: This needs to be refactored.
			string path = GetPrefabPath();

			AssetBundleRequest assetAsync = Bundle.LoadAssetAsync<GameObject>(path);

			assetAsync.completed += operation =>
			{
				//TODO: We're assuming success, is that bad?
				result.SetResult(assetAsync.asset as GameObject);
			};

			return result.Task;
		}
	}
}
