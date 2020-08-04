using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/discord/command")]
	public sealed class DiscordCommandController : AuthorizationReadyController
	{
		public DiscordCommandController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[ProducesJson]
		[HttpPost(GladMMOServiceDiscordConstants.EVENT_GRID_VALIDATE_ACTION)]
		public async Task<IActionResult> ValidateEventAsync([FromBody] EventGridSubscriptionEventModel[] eventModels)
		{
			//TODO: Validate URL
			return Json(new SubscriptionValidationResponse(eventModels.First().Data.ValidationCode));
		}
	}
}
