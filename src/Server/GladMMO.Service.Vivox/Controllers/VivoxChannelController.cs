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
	public class VivoxChannelController : AuthorizationReadyController
	{
		public VivoxChannelController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[AuthorizeJwt]
		[NoResponseCache]
		[HttpPost("proximity/join")]
		public async Task<IActionResult> JoinZoneProximityChat([FromServices] ITrinityCharacterRepository characterRepository,
			[FromServices] IFactoryCreatable<VivoxTokenClaims, VivoxTokenClaimsCreationContext> claimsFactory,
			[FromServices] IVivoxTokenSignService signService)
		{
			int accountId = this.ClaimsReader.GetAccountIdInt(User);

			//If the user doesn't actually have a claimed session in the game
			//then we shouldn't log them into Vivox.
			if (!await characterRepository.AccountHasActiveSession(accountId))
				return BuildFailedResponseModel(VivoxLoginResponseCode.NoActiveCharacterSession);

			int characterId = await RetrieveSessionCharacterIdAsync(characterRepository, accountId);

			Characters session = await characterRepository.RetrieveAsync((uint)characterId);

			//Players in the same zone will all join the same proximity channel such as Prox-1.
			//They can use this for proximity text and voice chat.
			//TODO: Support cases where a character is put into a different map than the DB says it should be in
			//TODO: Support instances, right now all instances share the same channel.
			//TODO: Use a factory for channel name generation maybe?
			VivoxTokenClaims claims = claimsFactory.Create(new VivoxTokenClaimsCreationContext(characterId, VivoxAction.JoinChannel, new VivoxChannelData(true, $"Prox-{session.Map}")));

			//We don't send it back in a JSON form even though it's technically a JSON object
			//because the client just needs it as a raw string anyway to put through the Vivox client API.
			return BuildSuccessfulResponseModel(new VivoxChannelJoinResponse(signService.CreateSignature(claims), claims.DestinationSIPURI));
		}

		[AuthorizeJwt]
		[NoResponseCache]
		[HttpPost("guild/join")]
		public async Task<IActionResult> JoinGuildChat([FromServices] ICharacterSessionRepository characterSessionRepository,
			[FromServices] IFactoryCreatable<VivoxTokenClaims, VivoxTokenClaimsCreationContext> claimsFactory,
			[FromServices] IGuildCharacterMembershipRepository guildMembershipRepository,
			[FromServices] IVivoxTokenSignService signService)
		{
			int accountId = this.ClaimsReader.GetAccountIdInt(User);

			return BuildFailedResponseModel(VivoxLoginResponseCode.ChannelUnavailable);

			/*//If the user doesn't actually have a claimed session in the game
			//then we shouldn't log them into Vivox.
			if(!await characterSessionRepository.AccountHasActiveSession(accountId))
				return BuildFailedResponseModel(VivoxLoginResponseCode.NoActiveCharacterSession);

			int characterId = await RetrieveSessionCharacterIdAsync(characterSessionRepository, accountId);

			if(!await guildMembershipRepository.ContainsAsync(characterId))
				return BuildFailedResponseModel(VivoxLoginResponseCode.ChannelUnavailable);

			int guildId = (await guildMembershipRepository.RetrieveAsync(characterId)).GuildId;

			//TODO: Use a factory for channel name generation maybe?
			VivoxTokenClaims claims = claimsFactory.Create(new VivoxTokenClaimsCreationContext(characterId, VivoxAction.JoinChannel, new VivoxChannelData(false, $"Guild-{guildId}")));

			//We don't send it back in a JSON form even though it's technically a JSON object
			//because the client just needs it as a raw string anyway to put through the Vivox client API.
			return BuildSuccessfulResponseModel(new VivoxChannelJoinResponse(signService.CreateSignature(claims), claims.DestinationSIPURI));*/
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