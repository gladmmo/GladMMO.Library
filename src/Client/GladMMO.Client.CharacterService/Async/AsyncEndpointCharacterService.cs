﻿using System; using FreecraftCore;
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
		public async Task<CharacterSessionDataResponse> GetCharacterSessionData(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterSessionData(characterId).ConfigureAwaitFalse();
		}

		public async Task<CharacterNameValidationResponse> ValidateName(string name)
		{
			return await (await GetService().ConfigureAwaitFalse()).ValidateName(name).ConfigureAwaitFalse();
		}

		public async Task<CharacterCreationResponse> CreateCharacter(CharacterCreationRequest request)
		{
			return await (await GetService().ConfigureAwaitFalse()).CreateCharacter(request).ConfigureAwaitFalse();
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