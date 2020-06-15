using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ILoadableContentResourceManager
	{
		UserContentType ContentType { get; }

		/// <summary>
		/// Attempts to load the <see cref="IPrefabContentResourceHandle"/>
		/// for an avatar with the key <see cref="avatarId"/>.
		/// </summary>
		/// <param name="avatarId">The avatar id.</param>
		/// <returns>Awaitable that will yield a prefab resource handle.</returns>
		Task<IPrefabContentResourceHandle> LoadContentPrefabAsync(long avatarId);
	}
}
