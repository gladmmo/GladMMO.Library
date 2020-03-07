using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IGameObjectDataServiceClient
	{
		[Get("/api/GameObjectTemplateData/template/{id}")]
		Task<ResponseModel<GameObjectTemplateModel, SceneContentQueryResponseCode>> GetGameObjectTemplate([AliasAs("id")] int gameObjectTemplateId);

		[Get("/api/GameObjectInstanceData/instance/{id}")]
		Task<ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode>> GetGameObjectInstance([AliasAs("id")] int gameObjectId);

		[Post("/api/GameObjectInstanceData/{worldId}/instance")]
		Task<ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode>> CreateGameObjectInstance(long worldId);

		[Put("/api/GameObjectInstanceData/instance/{id}")]
		Task UpdateGameObjectInstance([AliasAs("id")] int gameObjectId, [JsonBody] GameObjectInstanceModel model);

		/// <summary>
		/// REST endpoint that yields the entire collection of gameobject entries for a provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/api/GameObjectInstanceData/{worldId}/instance")]
		Task<ResponseModel<ObjectEntryCollectionModel<GameObjectInstanceModel>, ContentEntryCollectionResponseCode>> GetGameObjectEntriesByWorld(long worldId);

		/// <summary>
		/// REST endpoint that yields the entire collection of templates references in provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/api/GameObjectTemplateData/{worldId}/template")]
		Task<ResponseModel<ObjectEntryCollectionModel<GameObjectTemplateModel>, ContentEntryCollectionResponseCode>> GetGameObjectTemplatesByWorld(long worldId);
	}
}