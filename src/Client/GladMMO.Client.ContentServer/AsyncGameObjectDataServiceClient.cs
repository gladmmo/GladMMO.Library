using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncGameObjectDataServiceClient : BaseAsyncEndpointService<IGameObjectDataServiceClient>, IGameObjectDataServiceClient
	{
		/// <inheritdoc />
		public AsyncGameObjectDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncGameObjectDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<GameObjectTemplateModel, SceneContentQueryResponseCode>> GetGameObjectTemplate(int gameObjectTemplateId)
		{
			return await(await GetService().ConfigureAwait(false)).GetGameObjectTemplate(gameObjectTemplateId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode>> GetGameObjectInstance(int gameObjectId)
		{
			return await(await GetService().ConfigureAwait(false)).GetGameObjectInstance(gameObjectId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode>> CreateGameObjectInstance(long worldId)
		{
			return await(await GetService().ConfigureAwait(false)).CreateGameObjectInstance(worldId).ConfigureAwait(false);
		}

		public async Task UpdateGameObjectInstance(int gameObjectId, GameObjectInstanceModel model)
		{
			await(await GetService().ConfigureAwait(false)).UpdateGameObjectInstance(gameObjectId, model).ConfigureAwait(false);
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<GameObjectInstanceModel>, ContentEntryCollectionResponseCode>> GetGameObjectEntriesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwait(false)).GetGameObjectEntriesByWorld(worldId).ConfigureAwait(false);
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<GameObjectTemplateModel>, ContentEntryCollectionResponseCode>> GetGameObjectTemplatesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwait(false)).GetGameObjectTemplatesByWorld(worldId).ConfigureAwait(false);
		}
	}
}
