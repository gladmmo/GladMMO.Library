using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Proxy interface for zone registry controller/service.
	/// </summary>
	[Headers("User-Agent: GuardiansServer")]
	public interface IZoneRegistryService
	{
		[RequiresAuthentication]
		[Post("/api/ZoneRegistry/register")]
		Task<ResponseModel<ZoneServerRegistrationResponse, ZoneServerRegistrationResponseCode>> TryRegisterZoneServerAsync([JsonBody] ZoneServerRegistrationRequest request);

		[RequiresAuthentication]
		[Post("/api/ZoneRegistry/checkin")]
		Task ZoneServerCheckinAsync();
	}
}