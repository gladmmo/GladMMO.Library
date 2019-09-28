using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IPlayerSpawnPointDataServiceClient
	{
		[Get("/api/PlayerSpawnPointData/instance/{id}")]
		Task<ResponseModel<PlayerSpawnPointInstanceModel, SceneContentQueryResponseCode>> GetSpawnPointInstance([AliasAs("id")] int spawnPointId);

		[Post("/api/PlayerSpawnPointData/{worldId}/instance")]
		Task<ResponseModel<PlayerSpawnPointInstanceModel, SceneContentQueryResponseCode>> CreateSpawnPointInstance(long worldId);

		[Put("/api/PlayerSpawnPointData/instance/{id}")]
		Task UpdateSpawnPointInstance([AliasAs("id")] int spawnPointId, [JsonBody] PlayerSpawnPointInstanceModel model);

		/// <summary>
		/// REST endpoint that yields the entire collection of player spawn point entries for a provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/api/PlayerSpawnPointData/{worldId}/instance")]
		Task<ResponseModel<ObjectEntryCollectionModel<PlayerSpawnPointInstanceModel>, ContentEntryCollectionResponseCode>> GetSpawnPointEntriesByWorld(long worldId);
	}
}