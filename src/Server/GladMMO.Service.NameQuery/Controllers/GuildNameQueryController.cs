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
	[Route("api/namequery/Guild")]
	public class GuildNameQueryController : AuthorizationReadyController
	{
		private IGuildEntryRepository GuildRepository { get; }

		/// <inheritdoc />
		public GuildNameQueryController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger,
			[JetBrains.Annotations.NotNull] IGuildEntryRepository guildRepository)
			: base(claimsReader, logger)
		{
			GuildRepository = guildRepository ?? throw new ArgumentNullException(nameof(guildRepository));
		}

		[AllowAnonymous]
		[ProducesJson]
		[ResponseCache(Duration = 360)] //We want to cache this for a long time. But it's possible with name changes that we want to not cache forever
		[HttpGet("{id}/name")]
		public async Task<IActionResult> GuildNameQuery([FromRoute(Name = "id")] int guildId)
		{
			if (!await GuildRepository.ContainsAsync(guildId))
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			GuildEntryModel model = await GuildRepository.RetrieveAsync(guildId);

			return BuildSuccessfulResponseModel(new NameQueryResponse(model.GuildName));
		}
	}
}
