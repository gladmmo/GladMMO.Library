using System; using FreecraftCore;
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
			return await (await GetService().ConfigureAwaitFalse()).CreateZoneServerAccount(request).ConfigureAwaitFalse();
		}

		public async Task<JWTModel> TryAuthenticate(AuthenticationRequestModel request)
		{
			return await (await GetService().ConfigureAwaitFalse()).TryAuthenticate(request).ConfigureAwaitFalse();
		}
	}
}
