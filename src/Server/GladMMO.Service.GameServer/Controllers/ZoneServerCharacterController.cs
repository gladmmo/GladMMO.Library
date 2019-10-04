using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Controller for ZoneServer serverside actions involving characters
	/// or character data.
	/// </summary>
	[Route("api/[controller]")]
	public class ZoneServerCharacterController : AuthorizationReadyController
	{
		/// <inheritdoc />
		public ZoneServerCharacterController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		//TODO: Really not secure
		[ProducesJson]
		[HttpPost("WorldTeleport")]
		[NoResponseCache]
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)] //TODO: Eventually we'll need to auth these zoneservers.
		public async Task<IActionResult> WorldTeleportCharacter([FromBody] [NotNull] ZoneServerWorldTeleportCharacterRequest requestModel,
			[FromServices] [NotNull] ICharacterLocationRepository characterLocationRepository,
			[FromServices] [NotNull] IWorldTeleporterDataServiceClient worldTelporterDataClient,
			[FromServices] [NotNull] IPlayerSpawnPointDataServiceClient playerSpawnDataClient)
		{
			if (requestModel == null) throw new ArgumentNullException(nameof(requestModel));
			if (characterLocationRepository == null) throw new ArgumentNullException(nameof(characterLocationRepository));
			if (worldTelporterDataClient == null) throw new ArgumentNullException(nameof(worldTelporterDataClient));
			if (playerSpawnDataClient == null) throw new ArgumentNullException(nameof(playerSpawnDataClient));

			//TODO: Right now there is no verification of WHO/WHAT is actually teleporting the player.
			//We need an authorization system with player-owned zone servers. So that we can determine
			//who is requesting to transfer the session and then verify that a player is even on
			//that zone server.
			ProjectVersionStage.AssertBeta();

			//We don't await so that we can get rolling on this VERY async multi-part process.
			//TODO: Handle failure
			ResponseModel<WorldTeleporterInstanceModel, SceneContentQueryResponseCode> teleporterInstanceResponse = await worldTelporterDataClient.GetWorldTeleporterInstance(requestModel.WorldTeleporterId);

			//TODO: Handle failure
			ResponseModel<PlayerSpawnPointInstanceModel, SceneContentQueryResponseCode> pointInstanceResponse = await playerSpawnDataClient.GetSpawnPointInstance(teleporterInstanceResponse.Result.RemoteSpawnPointId);

			//Remove current location and update the new location.
			await characterLocationRepository.TryDeleteAsync(requestModel.CharacterGuid.EntityId);
			await characterLocationRepository.TryCreateAsync(new CharacterLocationModel(requestModel.CharacterGuid.EntityId,
				pointInstanceResponse.Result.InitialPosition.x,
				pointInstanceResponse.Result.InitialPosition.y,
				pointInstanceResponse.Result.InitialPosition.z,
				pointInstanceResponse.Result.WorldId));

			//TODO: Better indicate reason for failure.
			return Ok();
		}
	}
}
