using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnityEngine;

namespace GladMMO
{
	[Route("api/characters")]
	public class CharacterController : AuthorizationReadyController
	{
		//TODO: Check and enforce character limit
		//TODO: Add logging to these controllers
		private ICharacterRepository CharacterRepository { get; }

		/// <inheritdoc />
		public CharacterController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			ICharacterRepository characterRepository) 
			: base(claimsReader, logger)
		{
			if(characterRepository == null) throw new ArgumentNullException(nameof(characterRepository));

			CharacterRepository = characterRepository;
		}

		[ProducesJson]
		[ResponseCache(Duration = 10)] //Jagex crumbled for a day due to name checks. So, we should cache for 10 seconds. Probably won't change much.
		[AllowAnonymous]
		[HttpGet("name/{name}/validate")]
		public async Task<IActionResult> ValidateCharacterName([FromRoute] string name)
		{
			if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

			//TODO: Finer grain picking apart. We want to indicate the failure reason.
			bool nameIsAvailable = await ValidateNameAvailability(name);

			return Ok(new CharacterNameValidationResponse(nameIsAvailable ? CharacterNameValidationResponseCode.Success : CharacterNameValidationResponseCode.NameIsUnavailable));
		}

		private async Task<bool> ValidateNameAvailability(string name)
		{
			//TODO: Add a dependency that can filter and check the validate the name's format/characters/length

			//Now we have to check if a character exists with this name
			return !await CharacterRepository.ContainsAsync(name);
		}

		//TODO: Support recieve creation model JSON. Same with response.
		[ProducesJson]
		[AuthorizeJwt] //is it IMPORTANT that this method authorize the user. Don't know the accountid otherwise even, would be impossible.
		[HttpPost("create/{name}")]
		[NoResponseCache]
		public async Task<IActionResult> CreateCharacter([FromRoute] string name, 
			[FromServices] [NotNull] IPlayfabCharacterClient playfabCharacterClient,
			[FromServices] [NotNull] ICharacterAppearanceRepository characterAppearanceRepository,
			[FromServices] [NotNull] ICharacterDataRepository characterDataRepository)
		{
			if (playfabCharacterClient == null) throw new ArgumentNullException(nameof(playfabCharacterClient));
			if (characterAppearanceRepository == null) throw new ArgumentNullException(nameof(characterAppearanceRepository));
			if (characterDataRepository == null) throw new ArgumentNullException(nameof(characterDataRepository));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

			int accountId = ClaimsReader.GetAccountIdInt(User);

			bool nameIsAvailable = await ValidateNameAvailability(name);

			if(!nameIsAvailable)
				return BadRequest(new CharacterCreationResponse(CharacterCreationResponseCode.NameUnavailableError));

			string playfabId = ClaimsReader.GetPlayfabId(User);

			//Now, we actually need to create the character on PlayFab first. It's better to have an orphaned character on PlayFab
			//than to have a character without a PlayFab equivalent.
			PlayFabResultModel<GladMMOPlayFabGrantCharacterToUserResult> playFabResultModel = await playfabCharacterClient.GrantCharacterToUser(new GladMMOPlayFabGrantCharacterToUserRequest(name, "test", playfabId));

			//TODO: Better error handling
			if (playFabResultModel.ResultCode != HttpStatusCode.OK)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"PlayFab CharacterCreation Erorr: {playFabResultModel.ResultCode}:{playFabResultModel.ResultStatus}");

				return BadRequest(new CharacterCreationResponse(CharacterCreationResponseCode.GeneralServerError));
			}

			CharacterEntryModel characterEntryModel = new CharacterEntryModel(accountId, name, playfabId, playFabResultModel.Data.CharacterId);

			//TODO: We need a transition around the creation of the below entries.
			ProjectVersionStage.AssertBeta();
			//TODO: Don't expose the database table model
			//Otherwise we should try to create. There is a race condition here that can cause it to still fail
			//since others could create a character with this name before we finish after checking
			bool result = await CharacterRepository.TryCreateAsync(characterEntryModel);

			//TODO: Also needs to be apart of the transaction
			if(result)
			{
				await characterDataRepository.TryCreateAsync(new CharacterDataModel(characterEntryModel.CharacterId, 0));
				await characterAppearanceRepository.TryCreateAsync(new CharacterAppearanceModel(characterEntryModel.CharacterId, 9)); //Default is 9 right now.
			}

			return Json(new CharacterCreationResponse(CharacterCreationResponseCode.Success));
		}

		[HttpGet("{id}/appearance")]
		[AuthorizeJwt]
		[ProducesJson]
		public async Task<IActionResult> GetCharacterAppearance([FromRoute(Name = "id")] int characterId, [FromServices] [NotNull] ICharacterAppearanceRepository characterAppearanceRepository)
		{
			if (!await characterAppearanceRepository.ContainsAsync(characterId))
				return BuildFailedResponseModel(CharacterDataQueryReponseCode.CharacterNotFound);

			CharacterAppearanceModel appearanceModel = await characterAppearanceRepository.RetrieveAsync(characterId);

			return BuildSuccessfulResponseModel(new CharacterAppearanceResponse((int)appearanceModel.AvatarModelId));
		}

		[NoResponseCache]
		[HttpGet("{id}/data")]
		[AuthorizeJwt]
		[ProducesJson]
		public async Task<IActionResult> GetCharacterData([FromRoute(Name = "id")] int characterId, 
			[FromServices] [NotNull] ICharacterDataRepository characterDataRepository)
		{
			//TODO: We should only let the user themselves get their own character's data OR zoneservers who have a claimed session.
			ProjectVersionStage.AssertBeta();

			if(!await characterDataRepository.ContainsAsync(characterId))
				return BuildFailedResponseModel(CharacterDataQueryReponseCode.CharacterNotFound);

			CharacterDataModel characterData = await characterDataRepository.RetrieveAsync(characterId);

			return BuildSuccessfulResponseModel(new CharacterDataInstance(characterData.ExperiencePoints));
		}

		[HttpGet]
		[AuthorizeJwt]
		[ProducesJson]
		public async Task<CharacterListResponse> GetCharacters()
		{
			int accountId = ClaimsReader.GetAccountIdInt(User);

			//So to check characters we just need to query for the
			//characters with this account id
			int[] characterIds = await CharacterRepository.CharacterIdsForAccountId(accountId);

			if(characterIds.Length == 0)
				return new CharacterListResponse(CharacterListResponseCode.NoCharactersFoundError);
			
			//The reason we only provide the IDs is all other character data can be looked up
			//by the client when it needs it. Like name query, visible/character details/look stuff.
			//No reason to send all this data when they may only need names. Which can be queried through the known API
			return new CharacterListResponse(characterIds);
		}

		[ProducesJson]
		[HttpGet("location/{id}")]
		[NoResponseCache]
		//TODO: Renable ZoneServer authorization eventually.
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)]
		public async Task<IActionResult> GetCharacterLocation([FromRoute(Name = "id")] int characterId, [NotNull] [FromServices] ICharacterLocationRepository locationRepository)
		{
			if(locationRepository == null) throw new ArgumentNullException(nameof(locationRepository));

			if(characterId <= 0 || !await CharacterRepository.ContainsAsync(characterId)
				.ConfigureAwaitFalse())
				return Json(new ZoneServerCharacterLocationResponse(ZoneServerCharacterLocationResponseCode.CharacterDoesntExist));

			//So, the character exists and we now need to check if we can find a location for it. It may not have one, for whatever reason.
			//so we need to handle the case where it has none (maybe new character, or was manaully wiped).

			if(!await locationRepository.ContainsAsync(characterId).ConfigureAwaitFalse())
				return Json(new ZoneServerCharacterLocationResponse(ZoneServerCharacterLocationResponseCode.NoLocationDefined));

			//Otherwise, let's load and send the result
			CharacterLocationModel locationModel = await locationRepository.RetrieveAsync(characterId)
				.ConfigureAwaitFalse();

			//TODO: Integrate Map Id design into Schema, and implement it here.
			return Json(new ZoneServerCharacterLocationResponse(new Vector3(locationModel.XPosition, locationModel.YPosition, locationModel.ZPosition), 1));
		}

		//CharacterActionBarInstanceModel
		[ProducesJson]
		[HttpGet("{id}/actionbar")]
		[NoResponseCache]
		[AuthorizeJwt]
		public async Task<IActionResult> GetCharacterActionBar([FromRoute(Name = "id")] int characterId, [FromServices] ICharacterActionBarRepository actionBarRepository,
			[FromServices] ICharacterDefaultActionBarRepository characterDefaultActionBarRepository,
			[FromServices] ITypeConverterProvider<ICharacterActionBarEntry, CharacterActionBarInstanceModel> converter)
		{
			ProjectVersionStage.AssertBeta();
			//TODO: Check that they own the character.

			if (await actionBarRepository.ContainsAsync(characterId))
			{
				CharacterActionBarEntry[] actionBarEntries = await actionBarRepository.RetrieveAllForCharacterAsync(characterId);
				CharacterActionBarInstanceModel[] barInstanceModels = actionBarEntries.Select(converter.Convert)
					.ToArrayTryAvoidCopy();

				//Just send it as raw JSON.
				return Json(barInstanceModels);
			}
			else
			{
				//We need the default action bars
				//Right now we only have 1 class so let's use mage.
				CharacterDefaultActionBarEntry[] actionBarEntries = await characterDefaultActionBarRepository.RetrieveAllActionsAsync(EntityPlayerClassType.Mage);
				CharacterActionBarInstanceModel[] barInstanceModels = actionBarEntries.Select(converter.Convert)
					.ToArrayTryAvoidCopy();

				//TODO: Return default bars.
				return Json(barInstanceModels);
			}
		}
	}
}
