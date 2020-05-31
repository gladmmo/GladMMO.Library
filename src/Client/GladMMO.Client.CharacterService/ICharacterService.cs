using System; using FreecraftCore;
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
		/// Gets a character's session id, if authorized.
		/// </summary>
		/// <param name="characterId">The character id to get session data for.</param>
		/// <returns>The session data response.</returns>
		[RequiresAuthentication]
		[Get("/api/charactersession/{id}/data")]
		[Headers("Cache-Control: NoCache")] //TODO: I frgot what this is suppose to be
		Task<CharacterSessionDataResponse> GetCharacterSessionData([AliasAs("id")] int characterId);

		[RequiresAuthentication]
		[Get("/api/characters/name/{name}/validate")]
		Task<CharacterNameValidationResponse> ValidateName([AliasAs("name")] string name);

		//TODO: Accept more than name, we need creation/config details like class and race.
		[RequiresAuthentication]
		[Post("/api/characters/create")]
		Task<CharacterCreationResponse> CreateCharacter([JsonBody] CharacterCreationRequest request);

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
