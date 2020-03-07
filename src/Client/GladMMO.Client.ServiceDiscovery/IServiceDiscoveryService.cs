using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Contract for REST service that provides
	/// services discovery endpoints.
	/// </summary>
	[Headers("User-Agent: GuardiansClient")]
	public interface IServiceDiscoveryService
	{
		/// <summary>
		/// Attempts to discover a service with the provided <see cref="request"/>
		/// model.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>The result of the resolve request.</returns>
		[Post("/api/ServiceDiscovery/Discover")]
		Task<ResolveServiceEndpointResponse> DiscoverService([JsonBody] ResolveServiceEndpointRequest request);
	}
}
