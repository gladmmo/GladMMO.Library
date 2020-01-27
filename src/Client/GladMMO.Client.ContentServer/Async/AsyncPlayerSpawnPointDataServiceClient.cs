using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncPlayerSpawnPointDataServiceClient : BaseAsyncEndpointService<IPlayerSpawnPointDataServiceClient>, IPlayerSpawnPointDataServiceClient
	{
		/// <inheritdoc />
		public AsyncPlayerSpawnPointDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncPlayerSpawnPointDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<PlayerSpawnPointInstanceModel, SceneContentQueryResponseCode>> GetSpawnPointInstance(int spawnPointId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetSpawnPointInstance(spawnPointId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<PlayerSpawnPointInstanceModel, SceneContentQueryResponseCode>> CreateSpawnPointInstance(long worldId)
		{
			return await (await GetService().ConfigureAwaitFalse()).CreateSpawnPointInstance(worldId).ConfigureAwaitFalse();
		}

		public async Task UpdateSpawnPointInstance(int spawnPointId, PlayerSpawnPointInstanceModel model)
		{
			await (await GetService().ConfigureAwaitFalse()).UpdateSpawnPointInstance(spawnPointId, model).ConfigureAwaitFalseVoid();
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<PlayerSpawnPointInstanceModel>, ContentEntryCollectionResponseCode>> GetSpawnPointEntriesByWorld(long worldId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetSpawnPointEntriesByWorld(worldId).ConfigureAwaitFalse();
		}
	}
}
