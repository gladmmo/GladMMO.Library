using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Proxy interface for zone server data service requests.
	/// </summary>
	[Headers("User-Agent: GuardiansClient")]
	public interface IZoneDataService
	{
		//New zone stuff
		//TODO: We should add authorization to this
		[Get("/api/ZoneData/{id}/config")]
		[Headers("Cache-Control: max-age=300")]
		Task<ResponseModel<ZoneWorldConfigurationResponse, ZoneWorldConfigurationResponseCode>> GetZoneWorldConfigurationAsync([AliasAs("id")] int zoneId);

		//TODO: We should add authorization to this
		[Get("/api/ZoneData/{id}/endpoint")]
		[Headers("Cache-Control: max-age=300")]
		Task<ResolveServiceEndpointResponse> GetZoneConnectionEndpointAsync([AliasAs("id")] int zoneId);

		//TODO: We should add authorization to this
		[Get("/api/ZoneData/default/endpoint")]
		[Headers("Cache-Control: max-age=300")]
		Task<ResponseModel<ZoneConnectionEndpointResponse, ResolveServiceEndpointResponseCode>> GetAnyZoneConnectionEndpointAsync();
	}
}