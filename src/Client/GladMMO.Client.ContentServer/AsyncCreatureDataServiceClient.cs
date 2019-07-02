using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncCreatureDataServiceClient : BaseAsyncEndpointService<ICreatureDataServiceClient>, ICreatureDataServiceClient
	{
		/// <inheritdoc />
		public AsyncCreatureDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncCreatureDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<CreatureTemplateModel, SceneContentQueryResponseCode>> GetCreatureTemplate(int creatureTemplateId)
		{
			return await(await GetService().ConfigureAwait(false)).GetCreatureTemplate(creatureTemplateId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode>> GetCreatureInstance(int creatureId)
		{
			return await(await GetService().ConfigureAwait(false)).GetCreatureInstance(creatureId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode>> CreateCreatureInstance(long worldId)
		{
			return await(await GetService().ConfigureAwait(false)).CreateCreatureInstance(worldId).ConfigureAwait(false);

		}

		public async Task UpdateCreatureInstance(int creatureId, CreatureInstanceModel model)
		{
			await(await GetService().ConfigureAwait(false)).UpdateCreatureInstance(creatureId, model).ConfigureAwait(false);
		}

		public async Task<ResponseModel<CreatureEntryCollectionModel, CreatureCollectionResponseCode>> GetCreatureEntriesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwait(false)).GetCreatureEntriesByWorld(worldId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<CreatureTemplateCollectionModel, CreatureCollectionResponseCode>> GetCreatureTemplatesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwait(false)).GetCreatureTemplatesByWorld(worldId).ConfigureAwait(false);
		}
	}
}
