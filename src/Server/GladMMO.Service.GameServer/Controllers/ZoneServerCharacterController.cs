using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Controller for ZoneServer serverside actions involving characters
	/// or character data.
	/// </summary>
	[Route("api/[controller]")]
	public class ZoneServerCharacterController : AuthorizationReadyController
	{
		/// <inheritdoc />
		public ZoneServerCharacterController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}
	}
}
