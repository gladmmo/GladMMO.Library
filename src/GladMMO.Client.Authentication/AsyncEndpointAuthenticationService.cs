using System;
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
		public async Task<PlayerAccountJWTModel> TryAuthenticate(AuthenticationRequestModel request)
		{
			IAuthenticationService service = await GetService()
				.ConfigureAwait(false);

			return await service.TryAuthenticate(request)
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<string> TryRegister(string username, string password)
		{
			return await (await GetService().ConfigureAwait(false)).TryRegister(username, password).ConfigureAwait(false);
		}
	}
}
