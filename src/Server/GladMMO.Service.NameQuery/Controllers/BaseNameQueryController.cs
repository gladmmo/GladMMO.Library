using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public abstract class BaseNameQueryController : AuthorizationReadyController
	{
		/// <inheritdoc />
		protected BaseNameQueryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger)
			: base(claimsReader, logger)
		{

		}

		[AllowAnonymous]
		[ProducesJson]
		[ResponseCache(Duration = 360)] //We want to cache this for a long time. But it's possible with name changes that we want to not cache forever
		[HttpGet("{id}/name")]
		public async Task<IActionResult> EntityNameQuery([FromRoute(Name = "id")] ulong id)
		{
			//Since this is a GET we can't send a JSON model. We have to use this process instead, sending the raw guid value.
			ObjectGuid entityGuid = new ObjectGuid(id);
			return await EntityNameQuery(entityGuid);
		}

		protected abstract Task<JsonResult> EntityNameQuery(ObjectGuid entityGuid);
	}
}
