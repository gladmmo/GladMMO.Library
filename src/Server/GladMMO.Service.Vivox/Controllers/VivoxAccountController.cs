using System;
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

	}
}
