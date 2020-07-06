﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class GuildController : AuthorizationReadyController
	{
		/// <inheritdoc />
		public GuildController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger)
			: base(claimsReader, logger)
		{

		}

		//This endpoint allows for anon access because anyone should be able to see the guild status of a player.
		//Including armory/website and ingame players.
		[ProducesJson]
		[AllowAnonymous]
		[HttpGet("character/{id}")]
		[NoResponseCache] //we don't want to cache this, if they are removed from a guild then we want this reflected immediately or they may be taking in a guild chat they aren't apart of due to a race condition
		public async Task<IActionResult> GetCharacterMembershipGuildStatus([FromRoute(Name = "id")] int characterId,
			[NotNull] [FromServices] ITrinityGuildMembershipRepository guildCharacterMembershipRepository)
		{
			if (guildCharacterMembershipRepository == null) throw new ArgumentNullException(nameof(guildCharacterMembershipRepository));

			//If guild membership repo doesn't have the character id as an entry then it means there is no guild associated with them.
			if (!(await guildCharacterMembershipRepository.ContainsAsync((uint)characterId).ConfigureAwaitFalse()))
				return BuildFailedResponseModel(CharacterGuildMembershipStatusResponseCode.NoGuild);

			//TODO: There is technically a race condition here. They could have just been kicked from a guild but the cached model may say they are in a guild
			//this could result in incredibly rare cases of a kicked member joining at the perfect moment who can talk in guild chat but no longer be in the guild
			ProjectVersionStage.AssertBeta();

			//Otherwise, they are in a guild
			try
			{
				return BuildSuccessfulResponseModel(new CharacterGuildMembershipStatusResponse((int)(await guildCharacterMembershipRepository.RetrieveAsync((uint)characterId).ConfigureAwaitFalse()).Guildid));
			}
			catch (Exception e)
			{
				if (Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Encountered error in expected guild membership status request. CharacterId: {characterId} Reason: {e.Message}\n\nStack: {e.StackTrace}");

				return BuildFailedResponseModel(CharacterGuildMembershipStatusResponseCode.GeneralServerError);
			}
		}

		[ProducesJson]
		[AuthorizeJwt]
		[HttpGet("list")]
		public async Task<IActionResult> GetCharacterGuildList([NotNull] [FromServices] ITrinityGuildMembershipRepository guildCharacterMembershipRepository,
			[FromServices] ISocialServiceToGameServiceClient socialToGameClient)
		{
			if(guildCharacterMembershipRepository == null) throw new ArgumentNullException(nameof(guildCharacterMembershipRepository));

			CharacterSessionDataResponse session = await socialToGameClient.GetCharacterSessionDataByAccount(ClaimsReader.GetAccountIdInt(User));

			if (!session.isSuccessful)
				return BuildFailedResponseModel(CharacterGuildMembershipStatusResponseCode.GeneralServerError);

			//No guild check
			if (!await guildCharacterMembershipRepository.ContainsAsync((uint) session.CharacterId))
				return BuildFailedResponseModel(CharacterGuildMembershipStatusResponseCode.NoGuild);

			var playerGuildMembership = await guildCharacterMembershipRepository.RetrieveAsync((uint) session.CharacterId);

			int[] roster = await guildCharacterMembershipRepository.GetEntireGuildRosterAsync((int) playerGuildMembership.Guildid);

			return BuildSuccessfulResponseModel(new CharacterGuildListResponseModel(roster));
		}
	}
}
