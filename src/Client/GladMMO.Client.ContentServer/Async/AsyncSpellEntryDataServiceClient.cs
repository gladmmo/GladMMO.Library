using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncSpellEntryDataServiceClient : BaseAsyncEndpointService<ISpellEntryDataServiceClient>, ISpellEntryDataServiceClient
	{
		/// <inheritdoc />
		public AsyncSpellEntryDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncSpellEntryDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<SpellDefinitionCollectionResponseModel> GetDefaultSpellDataAsync()
		{
			return await (await GetService().ConfigureAwait(false)).GetDefaultSpellDataAsync().ConfigureAwait(false);
		}
	}
}
