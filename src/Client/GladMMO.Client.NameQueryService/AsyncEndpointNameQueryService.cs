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

		public async Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrievePlayerNameAsync(ulong rawGuidValue)
		{
			return await (await GetService().ConfigureAwait(false)).RetrievePlayerNameAsync(rawGuidValue).ConfigureAwait(false);
		}

		public async Task<ResponseModel<NetworkEntityGuid, NameQueryResponseCode>> RetrievePlayerGuidAsync(string characterName)
		{
			return await (await GetService().ConfigureAwait(false)).RetrievePlayerGuidAsync(characterName).ConfigureAwait(false);
		}

		public async Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrieveCreatureNameAsync(ulong rawGuidValue)
		{
			return await (await GetService().ConfigureAwait(false)).RetrieveCreatureNameAsync(rawGuidValue).ConfigureAwait(false);
		}

		public async Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrieveGameObjectNameAsync(ulong rawGuidValue)
		{
			return await (await GetService().ConfigureAwait(false)).RetrieveCreatureNameAsync(rawGuidValue).ConfigureAwait(false);
		}
	}
}
