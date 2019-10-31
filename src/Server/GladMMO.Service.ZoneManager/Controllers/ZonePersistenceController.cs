using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class ZonePersistenceController : AuthorizationReadyController
	{
		public ZonePersistenceController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}
	}
}
