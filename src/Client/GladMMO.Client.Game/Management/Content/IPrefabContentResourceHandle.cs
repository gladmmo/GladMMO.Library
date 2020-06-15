using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public interface IPrefabContentResourceHandle
	{
		/// <summary>
		/// Loads a prefab <see cref="GameObject"/>
		/// from the resource handle async.
		/// </summary>
		/// <returns>An awaitable that will contain the prefab.</returns>
		Task<GameObject> LoadPrefabAsync();
	}
}
