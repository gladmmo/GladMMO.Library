using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointZoneRegistryService : BaseAsyncEndpointService<IZoneRegistryService>, IZoneRegistryService
	{
		/// <inheritdoc />
		public AsyncEndpointZoneRegistryService(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{
		}

		/// <inheritdoc />
		public AsyncEndpointZoneRegistryService(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<ZoneServerRegistrationResponse, ZoneServerRegistrationResponseCode>> TryRegisterZoneServerAsync(ZoneServerRegistrationRequest request)
		{
			return await (await GetService().ConfigureAwait(false)).TryRegisterZoneServerAsync(request).ConfigureAwait(false);
		}

		public async Task ZoneServerCheckinAsync()
		{
			await (await GetService().ConfigureAwait(false)).ZoneServerCheckinAsync().ConfigureAwait(false);
		}
	}
}