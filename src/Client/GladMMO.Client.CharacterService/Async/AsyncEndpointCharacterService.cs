using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointCharacterService : BaseAsyncEndpointService<ICharacterService>, ICharacterService
	{
		/// <inheritdoc />
		public AsyncEndpointCharacterService(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{
		}

		/// <inheritdoc />
		public AsyncEndpointCharacterService(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		/// <inheritdoc />
		public async Task<CharacterListResponse> GetCharacters()
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacters().ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<CharacterSessionEnterResponse> TryEnterSession(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).TryEnterSession(characterId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<CharacterSessionDataResponse> GetCharacterSessionData(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterSessionData(characterId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<CharacterSessionEnterResponse> SetCharacterSessionData(int characterId, int zoneId)
		{
			return await (await GetService().ConfigureAwaitFalse()).SetCharacterSessionData(characterId, zoneId).ConfigureAwaitFalse();
		}

		public async Task<CharacterNameValidationResponse> ValidateName(string name)
		{
			return await (await GetService().ConfigureAwaitFalse()).ValidateName(name).ConfigureAwaitFalse();
		}

		public async Task<CharacterCreationResponse> CreateCharacter(string name)
		{
			return await (await GetService().ConfigureAwaitFalse()).CreateCharacter(name).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CharacterAppearanceResponse, CharacterDataQueryReponseCode>> GetCharacterAppearance(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterAppearance(characterId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CharacterDataInstance, CharacterDataQueryReponseCode>> GetCharacterData(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterData(characterId).ConfigureAwaitFalse();
		}

		public async Task<CharacterActionBarInstanceModel[]> GetCharacterActionBarDataAsync(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterActionBarDataAsync(characterId).ConfigureAwaitFalse();
		}
	}
}
