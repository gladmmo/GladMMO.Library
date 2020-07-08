﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class GossipTextController : AuthorizationReadyController
	{
		public GossipTextController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[HttpGet("creature/{id}")]
		[ResponseCache(Duration = 300)]
		public async Task<string> GetCreatureGossipText([FromRoute(Name = "id")] int textId, [FromServices] ITrinityCreatureTextRepository textRepository)
		{
			if (textId <= 0) throw new ArgumentOutOfRangeException(nameof(textId));

			if (!await textRepository.ContainsAsync((uint) textId))
				return "Greetings, $n."; //Hehe, the default.

			NpcText text = await textRepository.RetrieveAsync((uint) textId);

			//TODO: We should get the GUID so we can determine if this is male or female or whatever.
			return text.Text00;
		}
	}
}