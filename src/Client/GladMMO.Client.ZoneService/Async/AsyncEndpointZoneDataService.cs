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

		public async Task<long> GetZoneWorld(int zoneId)
		{
			return await(await GetService().ConfigureAwait(false)).GetZoneWorld(zoneId).ConfigureAwait(false);
		}

		public async Task<ResolveServiceEndpointResponse> GetServerEndpoint(int zoneId)
		{
			return await(await GetService().ConfigureAwait(false)).GetServerEndpoint(zoneId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<ZoneWorldConfigurationResponse, ZoneWorldConfigurationResponseCode>> GetZoneWorldConfigurationAsync(int zoneId)
		{
			return await (await GetService().ConfigureAwait(false)).GetZoneWorldConfigurationAsync(zoneId).ConfigureAwait(false);
		}

		public async Task<ResolveServiceEndpointResponse> GetZoneConnectionEndpointAsync(int zoneId)
		{
			return await (await GetService().ConfigureAwait(false)).GetZoneConnectionEndpointAsync(zoneId).ConfigureAwait(false);
		}
	}
}