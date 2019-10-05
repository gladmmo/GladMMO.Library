using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class VivoxChannelController : AuthorizationReadyController
	{
		public VivoxChannelController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[AuthorizeJwt]
		[NoResponseCache]
		[HttpPost("proximity/join")]
		public async Task<IActionResult> JoinZoneProximityChat([FromServices] ICharacterSessionRepository characterSessionRepository,
			[FromServices] IFactoryCreatable<VivoxTokenClaims, VivoxTokenClaimsCreationContext> claimsFactory,
			[FromServices] IVivoxTokenSignService signService)
		{
			int accountId = this.ClaimsReader.GetUserIdInt(User);

			//If the user doesn't actually have a claimed session in the game
			//then we shouldn't log them into Vivox.
			if (!await characterSessionRepository.AccountHasActiveSession(accountId))
				return BuildFailedResponseModel(VivoxLoginResponseCode.NoActiveCharacterSession);

			int characterId = await RetrieveSessionCharacterIdAsync(characterSessionRepository, accountId);

			CharacterSessionModel session = await characterSessionRepository.RetrieveAsync(characterId);

			//Players in the same zone will all join the same proximity channel such as Prox:1.
			//They can use this for proximity text and voice chat.
			//TODO: Use a factory for channel name generation maybe?
			VivoxTokenClaims claims = claimsFactory.Create(new VivoxTokenClaimsCreationContext(characterId, VivoxAction.JoinChannel, new VivoxChannelData(true, $"Prox:{session.ZoneId}")));

			//We don't send it back in a JSON form even though it's technically a JSON object
			//because the client just needs it as a raw string anyway to put through the Vivox client API.
			return BuildSuccessfulResponseModel(new VivoxChannelJoinResponse(signService.CreateSignature(claims), claims.DestinationSIPURI));
		}

		private static async Task<int> RetrieveSessionCharacterIdAsync(ICharacterSessionRepository characterSessionRepository, int accountId)
		{
			//TODO: Technically a race condition here.
			//Now let's actually get the character id of the session that the account has
			ClaimedSessionsModel session = await characterSessionRepository.RetrieveClaimedSessionByAccountId(accountId);
			int characterId = session.CharacterId;
			return characterId;
		}
	}
}