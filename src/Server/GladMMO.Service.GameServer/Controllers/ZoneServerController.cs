using Microsoft.AspNetCore.Mvc;
using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class ZoneServerController : AuthorizationReadyController
	{
		private IZoneServerRepository ZoneRepository { get; }
		/// <inheritdoc />
		public ZoneServerController([FromServices] IZoneServerRepository zoneRepository, IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{
			ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
		}

		//TODO: Create response model instead, incase the zoneserver doesn't exist.
		//We don't need to auth this, anyone can know the world.
		/// <summary>
		/// Returns the world (think map) of the zone.
		/// This can be used by clients to determine what world they should start downloading.
		/// </summary>
		/// <param name="zoneId"></param>
		/// <returns>The world id or a failure.</returns>
		[HttpGet("{id}/worldid")]
		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> GetZoneWorld([FromRoute(Name = "id")] int zoneId)
		{
			if(!await ZoneRepository.ContainsAsync(zoneId).ConfigureAwaitFalse())
			{
				Logger.LogError($"Failed to query for WorldId for Zone: {zoneId}");
				return NotFound();
			}

			ZoneInstanceEntryModel entryModel = await ZoneRepository.RetrieveAsync(zoneId)
				.ConfigureAwaitFalse();

			//We just return the world that this zone is for.
			return Ok(entryModel.WorldId);
		}
	}
}
