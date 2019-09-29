using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncWorldTeleporterDataServiceClient : BaseAsyncEndpointService<IWorldTeleporterDataServiceClient>, IWorldTeleporterDataServiceClient
	{
		/// <inheritdoc />
		public AsyncWorldTeleporterDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncWorldTeleporterDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}


		public async Task<ResponseModel<WorldTeleporterInstanceModel, SceneContentQueryResponseCode>> GetWorldTeleporterInstance(int gameObjectId)
		{
			return await (await GetService().ConfigureAwait(false)).GetWorldTeleporterInstance(gameObjectId).ConfigureAwait(false);
		}

		public async Task UpdateGameObjectInstance(int gameObjectId, WorldTeleporterInstanceModel model)
		{
			await (await GetService().ConfigureAwait(false)).UpdateGameObjectInstance(gameObjectId, model).ConfigureAwait(false);
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<WorldTeleporterInstanceModel>, ContentEntryCollectionResponseCode>> GetWorldTeleporterEntriesByWorld(long worldId)
		{
			return await (await GetService().ConfigureAwait(false)).GetWorldTeleporterEntriesByWorld(worldId).ConfigureAwait(false);
		}
	}
}
