using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSharpx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class CharacterSessionController : AuthorizationReadyController
	{
		private ICharacterRepository CharacterRepository { get; }

		private ICharacterSessionRepository CharacterSessionRepository { get; }

		/// <inheritdoc />
		public CharacterSessionController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, [FromServices] ICharacterRepository characterRepository, [FromServices] ICharacterSessionRepository characterSessionRepository) 
			: base(claimsReader, logger)
		{
			CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
			CharacterSessionRepository = characterSessionRepository ?? throw new ArgumentNullException(nameof(characterSessionRepository));
		}

		//TODO: This can't be unit tested because all the logic is written in an SQL stored procedure.
		/// <summary>
		/// Endpoint that the ZoneServers should query for attempted session claiming.
		/// ONLY zoneserver roles should be able to call this. NEVER allow clients to call this endpoint.
		/// </summary>
		/// <returns></returns>
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)]
		[HttpPost("claim")]
		public async Task<IActionResult> TryClaimSession([FromBody] ZoneServerTryClaimSessionRequest request)
		{
			//TODO: Renable auth for session claiming
			ProjectVersionStage.AssertAlpha();

			if(!this.ModelState.IsValid)
				return BadRequest(); //TODO: Send JSON back too.

			//TODO: We should validate a lot things. One, that the character has a session on this zoneserver.
			//We should also validate that the account owns the character. We need a new auth process for entering users.
			//We have to do this validation, somehow. Or malicious players could spoof this.
			ProjectVersionStage.AssertAlpha();

			//TODO: Verify that the zone id is correct. Right now we aren't providing it and the query doesn't enforce it.
			//We don't validate characterid/accountid association manually. It is implemented in the tryclaim SQL instead.
			//It additionally also checks the zone relation for the session so it will fail if it's invalid for the provided zone.
			//Therefore we don't need to make 3/4 database calls/queries to claim a session. Just one stored procedure call.
			//This is preferable. A result code will be used to indicate the exact error in the future. For now it just fails if it fails.
			bool sessionClaimed = await CharacterSessionRepository.TryClaimUnclaimedSession(request.PlayerAccountId, request.CharacterId);

			return Ok(new ZoneServerTryClaimSessionResponse(sessionClaimed ? ZoneServerTryClaimSessionResponseCode.Success : ZoneServerTryClaimSessionResponseCode.GeneralServerError)); //TODO
		}

		//This method name is abit misleading, it's more like "Create a new session data with the clients recommendation"
		//the client may want to go to a new world and zone, so they indicate what/where they want to do.
		//it's possible they aren't allowed to go there, and we should validate that and reject them.
		[HttpPost("{charid}/data")]
		[AuthorizeJwt]
		[NoResponseCache]
		public async Task<CharacterSessionEnterResponse> SetCharacterSessionData([FromRoute(Name = "charid")] int characterId, [FromBody] int zoneId)
		{
			if(!await VerifyCharacterOwnedByAccount(characterId))
			{
				//TODO: Return not authed in JSON
				return new CharacterSessionEnterResponse(CharacterSessionEnterResponseCode.InvalidCharacterIdError);
			}

			//This case is actually pretty likely, they will likely be trying to move to another server
			//before their active session is cleaned up. Retry logic will be required to get past this.
			if(await CharacterSessionRepository.AccountHasActiveSession(ClaimsReader.GetUserIdInt(User)))
			{
				//TODO: Return JSON that says active session
				return new CharacterSessionEnterResponse(CharacterSessionEnterResponseCode.AccountAlreadyHasCharacterSession);
			}

			//We can't check if it contains and then update, because that will there is a data race with
			//zoneserver deregisteration and cascading delteing.
			//So we only check for removal, and then remove and create

			//Don't try to delete the claimed session, we should try to delete the current session data. Since they want to change zones
			//and we consider that a new session
			if(!await CharacterSessionRepository.TryDeleteAsync(characterId))
			{
				//TODO: This could fail, potentially removed during race. But it's ok. It's ok that it's gone but may want to log it
			}

			//TODO: Refactor
			if(!await CharacterSessionRepository.TryCreateAsync(new CharacterSessionModel(characterId, zoneId)))
			{
				//Not sure what it wrong, no way to know really.
				return new CharacterSessionEnterResponse(CharacterSessionEnterResponseCode.GeneralServerError);
			}
				
			//It passed, they're allowed to join this zone.
			return new CharacterSessionEnterResponse(zoneId);
		}

		/// <summary>
		/// Verifies that the provided <see cref="characterId"/>
		/// is owned by the current User claim.
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		public async Task<bool> VerifyCharacterOwnedByAccount(int characterId)
		{
			int accountId = ClaimsReader.GetUserIdInt(User);

			//TODO: Do we want to expose this to non-controlers?
			//First we should validate that the account that is authorized owns the character it is requesting session data from

			return (await CharacterRepository.CharacterIdsForAccountId(accountId).ConfigureAwait(false))
				.Contains(characterId);
		}

		[HttpGet("{id}/data")]
		[AuthorizeJwt]
		[NoResponseCache]
		public async Task<CharacterSessionDataResponse> GetCharacterSessionData([FromRoute(Name = "id")] int characterId)
		{
			int accountId = ClaimsReader.GetUserIdInt(User);

			//TODO: Do we want to expose this to non-controlers?
			//First we should validate that the account that is authorized owns the character it is requesting session data from
			return await RetrieveSessionDataIfAvailable(characterId, accountId)
				.ConfigureAwait(false);
		}

		private async Task<CharacterSessionDataResponse> RetrieveSessionDataIfAvailable(int characterId, int accountId)
		{
			if(!(await CharacterRepository.CharacterIdsForAccountId(accountId).ConfigureAwait(false))
							.Contains(characterId))
			{
				//Requesting session data about an unowned character.
				return new CharacterSessionDataResponse(CharacterSessionDataResponseCode.Unauthorized);
			}

			//Active sessions don't matter, we just want session data for this character.
			if(await CharacterSessionRepository.ContainsAsync(characterId).ConfigureAwait(false))
			{
				//If there is a session, we should just send the zone. Maybe in the future we want to send more data but we only need the zone at the moment.
				return new CharacterSessionDataResponse((await CharacterSessionRepository.RetrieveAsync(characterId).ConfigureAwait(false)).ZoneId, characterId);
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
			if(!await CharacterSessionRepository.AccountHasActiveSession(accountId)
				.ConfigureAwait(false))
			{
				return Ok(new CharacterSessionDataResponse(CharacterSessionDataResponseCode.NoSessionAvailable));
			}

			//TODO: There is a dangerous race condition where the zoneserver can release a session inbetween the last databse call and the characte rsessin data call
			//This is unlikely to be exploitably but it is dangerous
			ProjectVersionStage.AssertBeta();

			try
			{
				ClaimedSessionsModel claimedSessionsModel = await CharacterSessionRepository.RetrieveClaimedSessionByAccountId(accountId)
					.ConfigureAwait(false);

				return Ok(new CharacterSessionDataResponse(claimedSessionsModel.Session.ZoneId, claimedSessionsModel.CharacterId));
			}
			catch(Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to query for character session data for active character session on AccountId: {accountId} Exception: {e.GetType().Name} - {e.Message}");

				return Ok(new CharacterSessionDataResponse(CharacterSessionDataResponseCode.GeneralServerError));
			}
		}


		/// <summary>
		/// Should be called by zone servers to release the active session on a character.
		/// When the active session exists the character cannot log into ANY other zone instance. They are stuck.
		/// So it is CRITICAL that the zoneserver does this.
		/// </summary>
		/// <param name="characterId">The ID of the character to free the session for.</param>
		/// <returns>OK if successful. Errors if not.</returns>
		[HttpDelete("{id}")]
		[NoResponseCache]
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)] //only zone servers should EVER be able to release the active session. They should also likely only be able to release an active session if it's on them.
		public async Task<IActionResult> ReleaseActiveSession([FromRoute(Name = "id")] int characterId)
		{
			//We NEED to AUTH for zoneserver JWT.
			ProjectVersionStage.AssertAlpha();

			//If an active session does NOT exist we have a BIG problem.
			if(!await CharacterSessionRepository.CharacterHasActiveSession(characterId))
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"ZoneServer requested ActiveSession for Player: {characterId} be removed. Session DOES NOT EXIST. This should NOT HAPPEN.");

				return NotFound();
			}

			//We should try to remove the active sesison.
			//One this active session is revoked the character/account is free to claim any existing session
			//including the same one that was just freed.
			if(!await CharacterSessionRepository.TryDeleteClaimedSession(characterId))
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"ZoneServer requested ActiveSession for Player: {characterId} be removed. Session DOES NOT EXIST. This should NOT HAPPEN.");

				return BadRequest();
			}
			else
			{
				if(Logger.IsEnabled(LogLevel.Information))
					Logger.LogInformation($"Removed ActiveSession for Player: {characterId}");

				return Ok();
			}
		}

		[HttpPost("enter/{id}")]
		[NoResponseCache]
		[AuthorizeJwt]
		public async Task<CharacterSessionEnterResponse> EnterSession([FromRoute(Name = "id")] int characterId)
		{
			if(!await IsCharacterIdValidForUser(characterId, CharacterRepository))
				return new CharacterSessionEnterResponse(CharacterSessionEnterResponseCode.InvalidCharacterIdError);

			int accountId = ClaimsReader.GetUserIdInt(User);

			//This checks to see if the account, not just the character, has an active session.
			//We do this before we check anything to reject quick even though the query behind this
			//may be abit more expensive
			//As a note, this checks (or should) CLAIMED SESSIONS. So, it won't prevent multiple session entries for an account
			//This is good because we actually use the left over session data to re-enter the instances on disconnect.
			if(await CharacterSessionRepository.AccountHasActiveSession(accountId))
				return new CharacterSessionEnterResponse(CharacterSessionEnterResponseCode.AccountAlreadyHasCharacterSession);

			//They may have a session entry already, which is ok. So long as they don't have an active claimed session
			//which the above query checks for.
			bool hasSession = await CharacterSessionRepository.ContainsAsync(characterId);

			//We need to check active or not
			if(hasSession)
			{
				//If it's active we can just retrieve the data and send them off on their way
				CharacterSessionModel sessionModel = await CharacterSessionRepository.RetrieveAsync(characterId);

				//TODO: Handle case when we have an inactive session that can be claimed
				return new CharacterSessionEnterResponse(sessionModel.ZoneId);
			}

			//If we've made it this far we'll need to create a session (because one does not exist) for the character
			//but we need player location data first (if they've never entered the world they won't have any
			//TODO: Handle location loading
			//TODO: Handle deafult
			if(!await CharacterSessionRepository.TryCreateAsync(new CharacterSessionModel(characterId, 1)))
				return new CharacterSessionEnterResponse(CharacterSessionEnterResponseCode.GeneralServerError);
			
			//TODO: Better zone handling
			return new CharacterSessionEnterResponse(1);
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
				(await characterRepository.RetrieveAsync(characterId)).AccountId == ClaimsReader.GetUserIdInt(User);
		}
	}
}
