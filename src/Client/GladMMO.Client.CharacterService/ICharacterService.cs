using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Proxy interface for character service requests.
	/// </summary>
	[Headers("User-Agent: GuardiansClient")]
	public interface ICharacterService
	{
		/// <summary>
		/// Requests a list of all available characters.
		/// </summary>
		/// <returns>The character request.</returns>
		[RequiresAuthentication]
		[Headers("Cache-Control: max-age=60")]
		[Get("/api/characters")]
		Task<CharacterListResponse> GetCharacters();

		/// <summary>
		/// TODO DOC
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		[RequiresAuthentication]
		[Post("/api/charactersession/enter/{id}")]
		Task<CharacterSessionEnterResponse> TryEnterSession([AliasAs("id")] int characterId);

		/// <summary>
		/// Gets a character's session id, if authorized.
		/// </summary>
		/// <param name="characterId">The character id to get session data for.</param>
		/// <returns>The session data response.</returns>
		[RequiresAuthentication]
		[Get("/api/charactersession/{id}/data")]
		[Headers("Cache-Control: NoCache")] //TODO: I frgot what this is suppose to be
		Task<CharacterSessionDataResponse> GetCharacterSessionData([AliasAs("id")] int characterId);

		//TODO: Doc
		/// <summary>
		/// Sets a character's session to the specified <see cref="zoneId"/>.
		/// This could fail, they may be an active session or it may not be allowed to travel to the specified zone.
		/// </summary>
		/// <param name="characterId"></param>
		/// <param name="zoneId"></param>
		/// <returns></returns>
		[RequiresAuthentication]
		[Post("/api/charactersession/{charid}/data")]
		Task<CharacterSessionEnterResponse> SetCharacterSessionData([AliasAs("charid")] int characterId, [JsonBody] int zoneId);

		[RequiresAuthentication]
		[Get("/api/characters/name/{name}/validate")]
		Task<CharacterNameValidationResponse> ValidateName([AliasAs("name")] string name);

		//TODO: Accept more than name, we need creation/config details like class and race.
		[RequiresAuthentication]
		[Post("/api/characters/create/{name}")]
		Task<CharacterCreationResponse> CreateCharacter([AliasAs("name")] string name);

		[Headers("Cache-Control: NoCache")]
		[RequiresAuthentication]
		[Get("/api/characters/{id}/appearance")]
		Task<ResponseModel<CharacterAppearanceResponse, CharacterDataQueryReponseCode>> GetCharacterAppearance([AliasAs("id")] int characterId);

		[Headers("Cache-Control: NoCache")]
		[RequiresAuthentication]
		[Get("/api/characters/{id}/data")]
		Task<ResponseModel<CharacterDataInstance, CharacterDataQueryReponseCode>> GetCharacterData([AliasAs("id")] int characterId);

		[Headers("Cache-Control: NoCache")]
		[RequiresAuthentication]
		[Get("/api/characters/{id}/actionbar")]
		Task<CharacterActionBarInstanceModel[]> GetCharacterActionBarDataAsync([AliasAs("id")] int characterId);
	}
}
