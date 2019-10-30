using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncZoneAuthenticationServiceClient : BaseAsyncEndpointService<IZoneAuthenticationService>, IZoneAuthenticationService
	{
		/// <inheritdoc />
		public AsyncZoneAuthenticationServiceClient(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncZoneAuthenticationServiceClient(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ZoneServerAccountRegistrationResponse> CreateZoneServerAccount(ZoneServerAccountRegistrationRequest request)
		{
			return await (await GetService().ConfigureAwait(false)).CreateZoneServerAccount(request).ConfigureAwait(false);
		}

		public async Task<JWTModel> TryAuthenticate(AuthenticationRequestModel request)
		{
			return await (await GetService().ConfigureAwait(false)).TryAuthenticate(request).ConfigureAwait(false);
		}
	}
}
