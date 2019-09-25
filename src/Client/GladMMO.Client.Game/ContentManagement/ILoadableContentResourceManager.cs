using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ILoadableContentResourceManager
	{
		/// <summary>
		/// Indicates if the avatar resource with key <see cref="contentId"/>
		/// is available and doesn't need to be downloaded.
		/// </summary>
		/// <param name="contentId">The avatar key.</param>
		/// <returns>True if the <see cref="IPrefabContentResourceHandle"/> for the provided avatar key is available in memory.</returns>
		bool IsContentResourceAvailable(long contentId);

		/// <summary>
		/// Attempts to load the <see cref="IPrefabContentResourceHandle"/>
		/// for an avatar with the key <see cref="avatarId"/>.
		/// </summary>
		/// <param name="avatarId">The avatar id.</param>
		/// <returns>Awaitable that will yield a prefab resource handle.</returns>
		Task<IPrefabContentResourceHandle> LoadContentPrefabAsync(long avatarId);

		/// <summary>
		/// Attempts to load a <see cref="IPrefabContentResourceHandle"/>
		/// from memory. If <see cref="IsContentResourceAvailable"/> is false
		/// then this will fail. Resources not in memory already must be gathered
		/// async from <see cref="LoadContentPrefabAsync"/>.
		/// </summary>
		/// <param name="contentId">The avatar key.</param>
		/// <returns>The prefab resource handle or null if it has not been downloaded.</returns>
		IPrefabContentResourceHandle TryLoadContentPrefab(long contentId);
	}
}
