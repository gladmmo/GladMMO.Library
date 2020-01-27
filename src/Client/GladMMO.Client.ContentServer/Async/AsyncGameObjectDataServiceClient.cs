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
			return await(await GetService().ConfigureAwaitFalse()).GetGameObjectTemplate(gameObjectTemplateId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode>> GetGameObjectInstance(int gameObjectId)
		{
			return await(await GetService().ConfigureAwaitFalse()).GetGameObjectInstance(gameObjectId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode>> CreateGameObjectInstance(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).CreateGameObjectInstance(worldId).ConfigureAwaitFalse();
		}

		public async Task UpdateGameObjectInstance(int gameObjectId, GameObjectInstanceModel model)
		{
			await(await GetService().ConfigureAwaitFalse()).UpdateGameObjectInstance(gameObjectId, model).ConfigureAwaitFalseVoid();
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<GameObjectInstanceModel>, ContentEntryCollectionResponseCode>> GetGameObjectEntriesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).GetGameObjectEntriesByWorld(worldId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<GameObjectTemplateModel>, ContentEntryCollectionResponseCode>> GetGameObjectTemplatesByWorld(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).GetGameObjectTemplatesByWorld(worldId).ConfigureAwaitFalse();
		}
	}
}
