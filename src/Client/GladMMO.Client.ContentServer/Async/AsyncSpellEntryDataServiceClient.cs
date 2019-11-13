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

		public async Task<SpellLevelLearnedCollectionResponseModel> GetLevelLearnedSpellsAsync()
		{
			return await (await GetService().ConfigureAwait(false)).GetLevelLearnedSpellsAsync().ConfigureAwait(false);
		}

		public async Task<SpellLevelLearnedCollectionResponseModel> GetLevelLearnedSpellsAsync(EntityPlayerClassType classType)
		{
			return await (await GetService().ConfigureAwait(false)).GetLevelLearnedSpellsAsync(classType).ConfigureAwait(false);
		}

		public async Task<SpellLevelLearnedCollectionResponseModel> GetLevelLearnedSpellsAsync(EntityPlayerClassType classType, int level)
		{
			return await (await GetService().ConfigureAwait(false)).GetLevelLearnedSpellsAsync(classType, level).ConfigureAwait(false);
		}
	}
}
