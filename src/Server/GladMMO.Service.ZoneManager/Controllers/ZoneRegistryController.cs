using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class ZoneRegistryController : AuthorizationReadyController
	{
		private IZoneServerRepository ZoneRepository { get; }

		/// <inheritdoc />
		public ZoneRegistryController([FromServices] IZoneServerRepository zoneRepository, 
			IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{
			ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
		}
	}
}
