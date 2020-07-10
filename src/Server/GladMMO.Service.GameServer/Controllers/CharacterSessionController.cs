using System; using FreecraftCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSharpx;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class CharacterSessionController : AuthorizationReadyController
	{
		private ITrinityCharactersRepository CharacterRepository { get; }

		/// <inheritdoc />
		public CharacterSessionController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			[FromServices] ITrinityCharactersRepository characterRepository) 
			: base(claimsReader, logger)
		{
			CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
		}

		/// <summary>
		/// Verifies that the provided <see cref="characterId"/>
		/// is owned by the current User claim.
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		public async Task<bool> VerifyCharacterOwnedByAccount(int characterId)
		{
			int accountId = ClaimsReader.GetAccountIdInt(User);

			//TODO: Do we want to expose this to non-controlers?
			//First we should validate that the account that is authorized owns the character it is requesting session data from

			return (await CharacterRepository.CharacterIdsForAccountId(accountId).ConfigureAwaitFalse())
				.Contains(characterId);
		}

		[HttpGet("{id}/data")]
		[AuthorizeJwt]
		[NoResponseCache]
		public async Task<CharacterSessionDataResponse> GetCharacterSessionData([FromRoute(Name = "id")] int characterId)
		{
			int accountId = ClaimsReader.GetAccountIdInt(User);

			//TODO: Do we want to expose this to non-controlers?
			//First we should validate that the account that is authorized owns the character it is requesting session data from
			return await RetrieveSessionDataIfAvailable(characterId, accountId)
				.ConfigureAwaitFalse();
		}

		private async Task<CharacterSessionDataResponse> RetrieveSessionDataIfAvailable(int characterId, int accountId)
		{
			if(!(await CharacterRepository.CharacterIdsForAccountId(accountId).ConfigureAwaitFalse())
							.Contains(characterId))
			{
				//Requesting session data about an unowned character.
				return new CharacterSessionDataResponse(CharacterSessionDataResponseCode.Unauthorized);
			}

			//Active sessions don't matter, we just want session data for this character.
			//TrinityCore: Above statement out of date, active sessions are ALL that matter now.
			if(await CharacterRepository.AccountHasActiveSession(accountId).ConfigureAwaitFalse())
			{
				//If there is a session, we should just send the zone. Maybe in the future we want to send more data but we only need the zone at the moment.
				return new CharacterSessionDataResponse((await CharacterRepository.RetrieveClaimedSessionByAccountId(accountId).ConfigureAwaitFalse()).Map, characterId);
			}
			else
				return new CharacterSessionDataResponse(CharacterSessionDataResponseCode.NoSessionAvailable);
		}

		[ProducesJson]
		[AuthorizeJwt(GuardianApplicationRole.SocialService)]
		[HttpGet("account/{id}/data")]
		[NoResponseCache]
		public async Task<IActionResult> GetCharacterSessionDataByAccount([FromRoute(Name = "id")] int accountId)
		{
			if(!await CharacterRepository.AccountHasActiveSession(accountId)
				.ConfigureAwaitFalse())
			{
				return Ok(new CharacterSessionDataResponse(CharacterSessionDataResponseCode.NoSessionAvailable));
			}

			//TODO: There is a dangerous race condition where the zoneserver can release a session inbetween the last databse call and the characte rsessin data call
			//This is unlikely to be exploitably but it is dangerous
			ProjectVersionStage.AssertBeta();

			try
			{
				Characters sessionCharacter = await CharacterRepository.RetrieveClaimedSessionByAccountId(accountId)
					.ConfigureAwaitFalse();

				return Ok(new CharacterSessionDataResponse(sessionCharacter.Map, (int)sessionCharacter.Guid));
			}
			catch(Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to query for character session data for active character session on AccountId: {accountId} Exception: {e.GetType().Name} - {e.Message}");

				return Ok(new CharacterSessionDataResponse(CharacterSessionDataResponseCode.GeneralServerError));
			}
		}

		/// <summary>
		/// Indicates if the provided character id is valid for the user in the message context.
		/// </summary>
		/// <param name="characterId">The id to check.</param>
		/// <param name="characterRepository">The character repository service.</param>
		/// <returns>True if the character id is valid/</returns>
		private async Task<bool> IsCharacterIdValidForUser(int characterId, ICharacterRepository characterRepository)
		{
			//We only support positive character ids so if they request a less than 0 it's invalid and likely spoofed
			//or if they request an id they don't own
			//or if it's an not a known character
			return characterId >= 0 &&
				await characterRepository.ContainsAsync(characterId) && 
				(await characterRepository.RetrieveAsync(characterId)).AccountId == ClaimsReader.GetAccountIdInt(User);
		}
	}
}
