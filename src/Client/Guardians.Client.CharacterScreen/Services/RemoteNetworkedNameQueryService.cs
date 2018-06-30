﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Guardians
{
	public sealed class RemoteNetworkedNameQueryService : INameQueryService
	{
		private ICharacterService CharacterService { get; }

		/// <inheritdoc />
		public RemoteNetworkedNameQueryService([NotNull] ICharacterService characterService)
		{
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
		}

		/// <inheritdoc />
		public string Retrieve(int id)
		{
			return RetrieveAsync(id).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public async Task<string> RetrieveAsync(int id)
		{
			CharacterNameQueryResponse queryResponse = await CharacterService.NameQuery(id)
				.ConfigureAwait(false);

			if(!queryResponse.isSuccessful)
				throw new KeyNotFoundException($"Failed to retrieve Key: {id} from {nameof(RemoteNetworkCharacterService)}. Error: {queryResponse.ResultCode}");

			return queryResponse.CharacterName;
		}
	}
}
