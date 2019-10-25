using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/namequery/" + nameof(EntityType.Player))]
	public class PlayerNameQueryController : BaseNameQueryController
	{
		private ICharacterRepository CharacterRepository { get; }

		/// <inheritdoc />
		public PlayerNameQueryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			ICharacterRepository characterRepository) 
			: base(claimsReader, logger)
		{
			CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
		}

		protected override async Task<JsonResult> EntityNameQuery(NetworkEntityGuid entityGuid)
		{
			if(entityGuid.EntityId <= 0)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			bool knownId = await CharacterRepository.ContainsAsync(entityGuid.EntityId);

			//TODO: JSON Response
			if(!knownId)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			//Else if it is a known id we should grab the name of the character
			string name = await CharacterRepository.RetrieveNameAsync(entityGuid.EntityId);

			return BuildSuccessfulResponseModel(new NameQueryResponse(name));
		}

		//return ResponseModel of NameQueryResponseCode and NetworkEntityGuid
		[AllowAnonymous]
		[ProducesJson]
		[ResponseCache(Duration = 360)] //We want to cache this for a long time. But it's possible with name changes that we want to not cache forever
		[HttpGet("{name}/reverse")]
		public async Task<IActionResult> ReverseNameQuery([FromRoute(Name = "name")] [JetBrains.Annotations.NotNull] string characterPlayerName)
		{
			if (string.IsNullOrWhiteSpace(characterPlayerName))
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			bool knownId = await CharacterRepository.ContainsAsync(characterPlayerName);

			//TODO: JSON Response
			if(!knownId)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			//Else if it is a known id we should grab the name of the character
			CharacterEntryModel characterModel = await CharacterRepository.RetrieveAsync(characterPlayerName);

			return BuildSuccessfulResponseModel(NetworkEntityGuidBuilder.New().WithType(EntityType.Player).WithId(characterModel.CharacterId));
		}
	}
}
