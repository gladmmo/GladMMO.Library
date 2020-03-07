using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IRegionbasedNameEndpointResolutionRepository
	{
		/// <summary>
		/// Attemptes to retrieve the <see cref="ResolvedEndpoint"/> from the repository mapped to the provided
		/// <see cref="locale"/> and <see cref="serviceType"/>.
		/// </summary>
		/// <param name="locale">The locale of the region the request is coming from.</param>
		/// <param name="serviceType">The type of serviced requested.</param>
		/// <exception cref="KeyNotFoundException">Thrown if the locale or service is unknown.</exception>
		/// <returns>A non-null <see cref="ResolvedEndpoint"/> pointing to the correct service of the specified service.</returns>
		Task<ResolvedEndpoint> RetrieveAsync(ClientRegionLocale locale, string serviceType);

		/// <summary>
		/// Indicates if the repository has entries for the specified <see cref="ClientRegionLocale"/> <see cref="locale"/>.
		/// </summary>
		/// <param name="locale">The locale to check for.</param>
		/// <returns>True if any entries exist for the specified <see cref="ClientRegionLocale"/> <see cref="locale"/>.</returns>
		Task<bool> HasDataForRegionAsync(ClientRegionLocale locale);

		/// <summary>
		/// Indicates if the repository has an entry for the specified <see cref="ClientRegionLocale"/> <see cref="locale"/>
		/// and the <see cref="serviceType"/>.
		/// </summary>
		/// <param name="locale">The locale to check for.</param>
		/// <param name="serviceType">The servicetype to check for.</param>
		/// <returns>True if an entry exists.</returns>
		Task<bool> HasEntryAsync(ClientRegionLocale locale, string serviceType);
	}
}
