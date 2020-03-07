using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IGameObjectBehaviourDataServiceClient<TBehaviourModelType> 
		where TBehaviourModelType : class
	{
		[Get("/instance/{id}")]
		Task<ResponseModel<TBehaviourModelType, SceneContentQueryResponseCode>> GetBehaviourInstance([AliasAs("id")] int gameObjectId);

		[Put("/instance/{id}")]
		Task UpdateBehaviourInstance([AliasAs("id")] int gameObjectId, [JsonBody] TBehaviourModelType model);

		/// <summary>
		/// REST endpoint that yields the entire collection of behaviour entries for a provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/{worldId}/instance")]
		Task<ResponseModel<ObjectEntryCollectionModel<TBehaviourModelType>, ContentEntryCollectionResponseCode>> GetBehaviourEntriesByWorld(long worldId);
	}
}
