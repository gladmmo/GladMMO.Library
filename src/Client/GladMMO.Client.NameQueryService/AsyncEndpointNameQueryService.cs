using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointNameQueryService : BaseAsyncEndpointService<INameQueryService>, INameQueryService
	{
		/// <inheritdoc />
		public AsyncEndpointNameQueryService(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{
		}

		/// <inheritdoc />
		public AsyncEndpointNameQueryService(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		/// <inheritdoc />
		public async Task<NameQueryResponse> RetrieveAsync(ulong rawGuidValue)
		{
			return await(await GetService().ConfigureAwait(false)).RetrieveAsync(rawGuidValue).ConfigureAwait(false);
		}
	}
}
