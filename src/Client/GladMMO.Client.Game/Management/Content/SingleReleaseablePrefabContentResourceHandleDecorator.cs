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

		public SingleReleaseablePrefabContentResourceHandleDecorator([NotNull] IPrefabContentResourceHandle decoratedHandle)
		{
			DecoratedHandle = decoratedHandle ?? throw new ArgumentNullException(nameof(decoratedHandle));
		}

		public Task<GameObject> LoadPrefabAsync()
		{
			return DecoratedHandle.LoadPrefabAsync();
		}
	}
}
