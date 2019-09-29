using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IWorldTeleporterDataServiceClient
	{
		[Get("/api/WorldTeleporterData/instance/{id}")]
		Task<ResponseModel<WorldTeleporterInstanceModel, SceneContentQueryResponseCode>> GetWorldTeleporterInstance([AliasAs("id")] int gameObjectId);

		[Put("/api/WorldTeleporterData/instance/{id}")]
		Task UpdateGameObjectInstance([AliasAs("id")] int gameObjectId, [JsonBody] WorldTeleporterInstanceModel model);

		/// <summary>
		/// REST endpoint that yields the entire collection of WorldTeleporter entries for a provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/api/WorldTeleporterData/{worldId}/instance")]
		Task<ResponseModel<ObjectEntryCollectionModel<WorldTeleporterInstanceModel>, ContentEntryCollectionResponseCode>> GetWorldTeleporterEntriesByWorld(long worldId);
	}
}
