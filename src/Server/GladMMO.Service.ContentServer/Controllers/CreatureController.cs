﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class CreatureController : BaseCustomContentController<CreatureModelEntryModel>
	{
		public CreatureController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger, UserContentType.Creature)
		{

		}

		protected override CreatureModelEntryModel GenerateNewModel()
		{
			int userId = ClaimsReader.GetAccountIdInt(User);
			return new CreatureModelEntryModel(userId, HttpContext.Connection.RemoteIpAddress.ToString(), Guid.NewGuid());
		}
	}
}
