using System;
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
			return await (await GetService().ConfigureAwait(false)).GetCharacters().ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<CharacterSessionEnterResponse> TryEnterSession(int characterId)
		{
			return await (await GetService().ConfigureAwait(false)).TryEnterSession(characterId).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<CharacterSessionDataResponse> GetCharacterSessionData(int characterId)
		{
			return await (await GetService().ConfigureAwait(false)).GetCharacterSessionData(characterId).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<CharacterSessionEnterResponse> SetCharacterSessionData(int characterId, int zoneId)
		{
			return await (await GetService().ConfigureAwait(false)).SetCharacterSessionData(characterId, zoneId).ConfigureAwait(false);
		}

		public async Task<CharacterNameValidationResponse> ValidateName(string name)
		{
			return await (await GetService().ConfigureAwait(false)).ValidateName(name).ConfigureAwait(false);
		}

		public async Task<CharacterCreationResponse> CreateCharacter(string name)
		{
			return await (await GetService().ConfigureAwait(false)).CreateCharacter(name).ConfigureAwait(false);
		}
	}
}
