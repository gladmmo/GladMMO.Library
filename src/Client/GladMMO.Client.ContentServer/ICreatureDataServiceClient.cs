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
	public interface ICreatureDataServiceClient
	{
		[Get("/api/CreatureTemplateData/template/{id}")]
		Task<ResponseModel<CreatureTemplateModel, SceneContentQueryResponseCode>> GetCreatureTemplate([AliasAs("id")] int creatureTemplateId);

		[Get("/api/CreatureInstanceData/instance/{id}")]
		Task<ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode>> GetCreatureInstance([AliasAs("id")] int creatureId);

		[Post("/api/CreatureInstanceData/{worldId}/instance")]
		Task<ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode>> CreateCreatureInstance(long worldId);

		[Put("/api/CreatureInstanceData/instance/{id}")]
		Task UpdateCreatureInstance([AliasAs("id")] int creatureId, [JsonBody] CreatureInstanceModel model);

		/// <summary>
		/// REST endpoint that yields the entire collection of creature entries for a provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/api/CreatureInstanceData/{worldId}/instance")]
		Task<ResponseModel<InstanceObjectEntryCollectionModel<CreatureInstanceModel>, CreatureCollectionResponseCode>> GetCreatureEntriesByWorld(long worldId);

		/// <summary>
		/// REST endpoint that yields the entire collection of templates references in provided/specified <see cref="worldId"/>.
		/// </summary>
		/// <param name="worldId">The id of the world.</param>
		/// <returns>A non-null response model indicating the success or result.</returns>
		[Get("/api/CreatureTemplateData/{worldId}/template")]
		Task<ResponseModel<CreatureTemplateCollectionModel, CreatureCollectionResponseCode>> GetCreatureTemplatesByWorld(long worldId);
	}
}