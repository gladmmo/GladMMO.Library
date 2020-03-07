using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class ZonePersistenceController : AuthorizationReadyController
	{
		private ICharacterSessionRepository CharacterSessionRepository { get; }

		public ZonePersistenceController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger,
			[NotNull] ICharacterSessionRepository characterSessionRepository)
			: base(claimsReader, logger)
		{
			CharacterSessionRepository = characterSessionRepository ?? throw new ArgumentNullException(nameof(characterSessionRepository));
		}

		//TODO: Document
		[AuthorizeJwt(GuardianApplicationRole.ZoneServer)] //important that only zoneservers can call this.
		[HttpPatch("{id}/data")]
		[ProducesJson]
		public async Task<IActionResult> SaveFullCharacterDataAsync([FromRoute(Name = "id")] int characterId, 
			[FromBody] [NotNull] FullCharacterDataSaveRequest request,
			[NotNull] [FromServices] ICharacterLocationRepository locationRepository,
			[NotNull] [FromServices] IZoneServerRepository zoneRepository,
			[FromServices] [NotNull] ICharacterDataRepository characterDataRepository)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			//TODO: Is it safe to not do this as a transaction??
			//TODO: For HTTP callers we should maybe include better information. Though with message queue we can't respond.
			if (!await CheckZoneAuthorizedToModifyCharacterData(characterId))
				return Forbid();

			//Don't always want to save the position of the user.
			if(request.isPositionSaved)
				await UpdateCharacterLocation(characterId, request.CharacterLocationData, locationRepository, zoneRepository);

			//TODO: Probably need to handle this abit better and more data than just experience.
			//Entity data can now be saved.
			await UpdatePlayerData(characterId, new CharacterDataInstance(request.PlayerDataSnapshot.GetFieldValue<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE)), characterDataRepository);

			if (request.ShouldReleaseCharacterSession)
				await CharacterSessionRepository.TryDeleteClaimedSession(characterId);

			return Ok();
		}

		//TODO: Secure this so only authenticated zoneservers who own a claimed session of for this character can update their data.
		//[ProducesJson]
		//[HttpPatch("{id}/UpdateData")]
		//[NoResponseCache]
		private async Task<IActionResult> UpdatePlayerData([FromRoute(Name = "id")] int characterId, 
			[FromBody] [NotNull] CharacterDataInstance requestModel,
			[FromServices] [NotNull] ICharacterDataRepository characterDataRepository)
		{
			CharacterDataModel dataModel = await characterDataRepository.RetrieveAsync(characterId);

			//TODO: Make this updatable
			dataModel.ExperiencePoints = requestModel.Experience;

			await characterDataRepository.UpdateAsync(characterId, dataModel);

			return Ok();
		}

		private async Task<bool> CheckZoneAuthorizedToModifyCharacterData(int characterId)
		{
			if (!await CharacterSessionRepository.CharacterHasActiveSession(characterId))
				return false;

			CharacterSessionModel model = await CharacterSessionRepository.RetrieveAsync(characterId);

			//If they aren't in this zone we shouldn't be allowed to save it.
			return ClaimsReader.GetAccountIdInt(User) == model.ZoneId;
		}

		[AuthorizeJwt(GuardianApplicationRole.ZoneServer)] //important that only zoneservers can call this.
		[HttpPatch("{id}/location")]
		public async Task<IActionResult> UpdateCharacterLocation(
			[FromRoute(Name = "id")] int characterId,
			[FromBody] ZoneServerCharacterLocationSaveRequest saveRequest,
			[NotNull] [FromServices] ICharacterLocationRepository locationRepository,
			[NotNull] [FromServices] IZoneServerRepository zoneRepository)
		{
			if(locationRepository == null) throw new ArgumentNullException(nameof(locationRepository));
			if (zoneRepository == null) throw new ArgumentNullException(nameof(zoneRepository));

			//TODO: For HTTP callers we should maybe include better information. Though with message queue we can't respond.
			if(!await CheckZoneAuthorizedToModifyCharacterData(characterId))
				return Forbid();

			//TODO: This could fail if we unregistered. More gracefully handle that case.
			//Get world, we need it for location
			ZoneInstanceEntryModel zoneEntry = await zoneRepository.RetrieveAsync(ClaimsReader.GetAccountIdInt(User));

			//If world was deleted then they won't have location.
			//TODO: Is this the best way to deal with this?
			if(await locationRepository.ContainsAsync(characterId).ConfigureAwaitFalse())
				await locationRepository.UpdateAsync(characterId, BuildCharacterLocationFromSave(characterId, saveRequest, zoneEntry.WorldId))
					.ConfigureAwaitFalseVoid();
			else
				await locationRepository.TryCreateAsync(BuildCharacterLocationFromSave(characterId, saveRequest, zoneEntry.WorldId))
					.ConfigureAwaitFalse();

			return Ok();
		}

		private static CharacterLocationModel BuildCharacterLocationFromSave(int characterId, ZoneServerCharacterLocationSaveRequest saveRequest, long mapId)
		{
			return new CharacterLocationModel(characterId, saveRequest.Position.x, saveRequest.Position.y, saveRequest.Position.z, mapId);
		}
	}
}