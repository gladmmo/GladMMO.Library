using System; using FreecraftCore;
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
			return await(await GetService().ConfigureAwaitFalse()).GetCreatureTemplate(creatureTemplateId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode>> GetCreatureInstance(int creatureId)
		{
			return await(await GetService().ConfigureAwaitFalse()).GetCreatureInstance(creatureId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode>> CreateCreatureInstance(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).CreateCreatureInstance(worldId).ConfigureAwaitFalse();

		}

		public async Task UpdateCreatureInstance(int creatureId, CreatureInstanceModel model)
		{
			await(await GetService().ConfigureAwaitFalse()).UpdateCreatureInstance(creatureId, model).ConfigureAwaitFalseVoid();
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<CreatureInstanceModel>, ContentEntryCollectionResponseCode>> GetCreatureEntriesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).GetCreatureEntriesByWorld(worldId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<CreatureTemplateModel>, ContentEntryCollectionResponseCode>> GetCreatureTemplatesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).GetCreatureTemplatesByWorld(worldId).ConfigureAwaitFalse();
		}
	}
}
