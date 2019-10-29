using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/ZoneData")]
	public class ZoneDataController : AuthorizationReadyController
	{
		private IZoneServerRepository ZoneRepository { get; }

		/// <inheritdoc />
		public ZoneDataController([FromServices] IZoneServerRepository zoneRepository, IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{
			ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
		}

		/// <summary>
		/// Returns the world (think map) of the zone.
		/// This can be used by clients to determine what world they should start downloading.
		/// </summary>
		/// <param name="zoneId"></param>
		/// <returns>The world id or a failure.</returns>
		[HttpGet("{id}/config")]
		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> GetZoneWorld([FromRoute(Name = "id")] int zoneId)
		{
			if(!await ZoneRepository.ContainsAsync(zoneId).ConfigureAwait(false))
			{
				Logger.LogError($"Failed to query for WorldId for Zone: {zoneId}");
				return NotFound();
			}

			ZoneInstanceEntryModel entryModel = await ZoneRepository.RetrieveAsync(zoneId)
				.ConfigureAwait(false);

			//We just return the world that this zone is for.
			return Ok(entryModel.WorldId);
		}

		//We don't require authorization because it's not unique per-player
		//It's also not a secret. They could auth then grab every endpoint.
		//No reason to try to hide it.
		[HttpGet("{id}/endpoint")]
		[ProducesJson]
		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> GetServerEndpoint([FromRoute(Name = "id")] int zoneId)
		{
			if(!ModelState.IsValid)
				return BadRequest(new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.GeneralRequestError));

			//We reuse the service discovery response model
			if(!await ZoneRepository.ContainsAsync(zoneId))
				return BadRequest(new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.ServiceUnlisted));

			//Small interval for race condition. So we try catch.
			try
			{
				ZoneInstanceEntryModel zone = await ZoneRepository.RetrieveAsync(zoneId);

				//Should be good, we just send them the endpoint
				if(zone != null)
					return Ok(new ResolveServiceEndpointResponse(new ResolvedEndpoint(zone.ZoneServerAddress, zone.ZoneServerPort)));
			}
			catch(Exception)
			{
				//TODO: Logging/event
			}

			return BadRequest(new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.GeneralRequestError));
		}
	}
}
