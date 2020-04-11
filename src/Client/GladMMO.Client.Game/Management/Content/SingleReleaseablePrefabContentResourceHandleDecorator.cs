using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Decorates the <see cref="IPrefabContentResourceHandle"/> with semantics
	/// that make it so that it can only ever be 
	/// </summary>
	public sealed class SingleReleaseablePrefabContentResourceHandleDecorator : IPrefabContentResourceHandle
	{
		private IPrefabContentResourceHandle DecoratedHandle { get; }

		private bool isReleased { get; set; } = false;

		private readonly object SyncObj = new object();

		public SingleReleaseablePrefabContentResourceHandleDecorator([NotNull] IPrefabContentResourceHandle decoratedHandle)
		{
			DecoratedHandle = decoratedHandle ?? throw new ArgumentNullException(nameof(decoratedHandle));
		}

		public int CurrentUseCount => DecoratedHandle.CurrentUseCount;

		public void Release()
		{
			if (isReleased)
				return;

			lock (SyncObj)
			{
				//Double check locking.
				if (isReleased)
					return;

				DecoratedHandle.Release();
				isReleased = true;
			}
		}

		public GameObject LoadPrefab()
		{
			return DecoratedHandle.LoadPrefab();
		}

		public Task<GameObject> LoadPrefabAsync()
		{
			return DecoratedHandle.LoadPrefabAsync();
		}
	}
}
