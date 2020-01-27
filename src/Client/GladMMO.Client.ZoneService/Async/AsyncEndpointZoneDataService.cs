using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointZoneDataService : BaseAsyncEndpointService<IZoneDataService>, IZoneDataService
	{
		/// <inheritdoc />
		public AsyncEndpointZoneDataService(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{
		}

		/// <inheritdoc />
		public AsyncEndpointZoneDataService(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<ZoneWorldConfigurationResponse, ZoneWorldConfigurationResponseCode>> GetZoneWorldConfigurationAsync(int zoneId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetZoneWorldConfigurationAsync(zoneId).ConfigureAwaitFalse();
		}

		public async Task<ResolveServiceEndpointResponse> GetZoneConnectionEndpointAsync(int zoneId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetZoneConnectionEndpointAsync(zoneId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ZoneConnectionEndpointResponse, ResolveServiceEndpointResponseCode>> GetAnyZoneConnectionEndpointAsync()
		{
			return await (await GetService().ConfigureAwaitFalse()).GetAnyZoneConnectionEndpointAsync().ConfigureAwaitFalse();
		}
	}
}