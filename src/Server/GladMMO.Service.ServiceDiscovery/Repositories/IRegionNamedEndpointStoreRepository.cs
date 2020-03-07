using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IRegionNamedEndpointStoreRepository
	{
		/// <summary>
		/// Loads the <see cref="NameEndpointResolutionStorageModel"/> for the corresponding <see cref="region"/>.
		/// </summary>
		/// <param name="region">Region/locale to load.</param>
		/// <returns>The storage model if it's available.</returns>
		Task<NameEndpointResolutionStorageModel> Retrieve(ClientRegionLocale region);

		/// <summary>
		/// Indicates if the specified <see cref="region"/> is available in the store.
		/// </summary>
		/// <param name="region">The region/locale to check.</param>
		/// <returns>True if the specified <see cref="region"/> is in the store.</returns>
		Task<bool> HasRegionStore(ClientRegionLocale region);
	}
}
