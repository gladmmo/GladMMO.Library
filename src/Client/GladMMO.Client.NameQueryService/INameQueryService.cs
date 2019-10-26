using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: GuardiansClient")]
	public interface INameQueryService
	{
		/// <summary>
		/// Retrieves the name of the player entity
		/// from the provided <see cref="rawGuidValue"/>.
		/// </summary>
		/// <param name="rawGuidValue">The id of the player entity.</param>
		/// <returns>Result of the player namequery.</returns>
		[Headers("Cache-Control: max-age=600")]
		//[Get("/api/namequery/" + nameof(EntityType.Player) + "/{EntityGuid}/name")]
		[Get("/api/namequery/Player/{EntityGuid}/name")]
		Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrievePlayerNameAsync([AliasAs("EntityGuid")] ulong rawGuidValue);

		/// <summary>
		/// Does a reverse name query for a player entity by name.
		/// </summary>
		/// <param name="characterName">The player/character name.</param>
		/// <returns></returns>
		[Headers("Cache-Control: max-age=600")]
		//[Get("/api/namequery/" + nameof(EntityType.Player) + "/{EntityGuid}/name")]
		[Get("/api/namequery/Player/{name}/reverse")]
		Task<ResponseModel<NetworkEntityGuid, NameQueryResponseCode>> RetrievePlayerGuidAsync([AliasAs("name")] string characterName);

		/// <summary>
		/// Retrieves the name of the creature entity
		/// from the provided <see cref="rawGuidValue"/>.
		/// </summary>
		/// <param name="rawGuidValue">The id of the creature entity.</param>
		/// <returns>Result of the creature namequery.</returns>
		[Headers("Cache-Control: max-age=600")]
		[Get("/api/namequery/Creature/{EntityGuid}/name")]
		Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrieveCreatureNameAsync([AliasAs("EntityGuid")] ulong rawGuidValue);

		/// <summary>
		/// Retrieves the name of the gameobject entity
		/// from the provided <see cref="rawGuidValue"/>.
		/// </summary>
		/// <param name="rawGuidValue">The id of the gameobject entity.</param>
		/// <returns>Result of the gameobject namequery.</returns>
		[Headers("Cache-Control: max-age=600")]
		[Get("/api/namequery/GameObject/{EntityGuid}/name")]
		Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrieveGameObjectNameAsync([AliasAs("EntityGuid")] ulong rawGuidValue);

		/// <summary>
		/// Retrieves the name of the guild entity
		/// from the provided <see cref="guildId"/>.
		/// </summary>
		/// <param name="guildId">The id of the guild.</param>
		/// <returns>Result of the guild namequery.</returns>
		[Headers("Cache-Control: max-age=600")]
		[Get("/api/namequery/Guild/{id}/name")]
		Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> RetrieveGuildNameAsync([AliasAs("id")] int guildId);
	}
}
