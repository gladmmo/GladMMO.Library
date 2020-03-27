using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Refit;

namespace GladMMO
{
	//
	public sealed class AsyncEndpointAuthenticationService : BaseAsyncEndpointService<IAuthenticationService>, IAuthenticationService
	{
		/// <inheritdoc />
		public AsyncEndpointAuthenticationService([NotNull] Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		public AsyncEndpointAuthenticationService([NotNull] Task<string> futureEndpoint, [NotNull] RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		/// <inheritdoc />
		public async Task<JWTModel> TryAuthenticate(AuthenticationRequestModel request)
		{
			IAuthenticationService service = await GetService()
				.ConfigureAwaitFalse();

			return await service.TryAuthenticate(request)
				.ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<string> TryRegister(string username, string password)
		{
			return await (await GetService().ConfigureAwaitFalse()).TryRegister(username, password).ConfigureAwaitFalse();
		}
	}
}
