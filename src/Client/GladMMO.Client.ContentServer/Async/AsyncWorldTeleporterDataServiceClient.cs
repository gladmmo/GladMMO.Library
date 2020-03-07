using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncGameObjectBehaviourDataServiceClient<TBehaviourType> : BaseAsyncEndpointService<IGameObjectBehaviourDataServiceClient<TBehaviourType>>, IGameObjectBehaviourDataServiceClient<TBehaviourType> 
		where TBehaviourType : class
	{
		/// <inheritdoc />
		public AsyncGameObjectBehaviourDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncGameObjectBehaviourDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}


		public async Task<ResponseModel<TBehaviourType, SceneContentQueryResponseCode>> GetBehaviourInstance(int gameObjectId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetBehaviourInstance(gameObjectId).ConfigureAwaitFalse();
		}

		public async Task UpdateBehaviourInstance(int gameObjectId, TBehaviourType model)
		{
			await (await GetService().ConfigureAwaitFalse()).UpdateBehaviourInstance(gameObjectId, model).ConfigureAwaitFalseVoid();
		}

		public async Task<ResponseModel<ObjectEntryCollectionModel<TBehaviourType>, ContentEntryCollectionResponseCode>> GetBehaviourEntriesByWorld(long worldId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetBehaviourEntriesByWorld(worldId).ConfigureAwaitFalse();
		}
	}
}
