using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class VivoxAccountController : AuthorizationReadyController
	{
		public VivoxAccountController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[AuthorizeJwt]
		[NoResponseCache]
		[HttpPost("Login")]
		public async Task<IActionResult> LoginVivox([FromServices] ITrinityCharacterRepository characterRepository,
			[FromServices] IFactoryCreatable<VivoxTokenClaims, VivoxTokenClaimsCreationContext> claimsFactory,
			[FromServices] IVivoxTokenSignService signService)
		{
			int accountId = this.ClaimsReader.GetAccountIdInt(User);

			//If the user doesn't actually have a claimed session in the game
			//then we shouldn't log them into Vivox.
			if (!await characterRepository.AccountHasActiveSession(accountId))
				return BuildFailedResponseModel(VivoxLoginResponseCode.NoActiveCharacterSession);

			int characterId = await RetrieveSessionCharacterIdAsync(characterRepository, accountId);

			VivoxTokenClaims claims = claimsFactory.Create(new VivoxTokenClaimsCreationContext(characterId, VivoxAction.Login));

			//We don't send it back in a JSON form even though it's technically a JSON object
			//because the client just needs it as a raw string anyway to put through the Vivox client API.
			return BuildSuccessfulResponseModel(signService.CreateSignature(claims));
		}

		private static async Task<int> RetrieveSessionCharacterIdAsync(ITrinityCharacterRepository characterRepository, int accountId)
		{
			//TODO: Technically a race condition here.
			//Now let's actually get the character id of the session that the account has
			Characters session = await characterRepository.RetrieveClaimedSessionByAccountId(accountId);
			int characterId = (int) session.Guid;
			return characterId;
		}
	}
}