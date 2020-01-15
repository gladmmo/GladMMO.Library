using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
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
		[ProducesJson]
		public async Task<IActionResult> GetZoneWorldConfiguration([FromRoute(Name = "id")] int zoneId)
		{
			if(!await ZoneRepository.ContainsAsync(zoneId).ConfigureAwait(false))
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to query for WorldId for Zone: {zoneId}");

				return BuildFailedResponseModel(ZoneWorldConfigurationResponseCode.ZoneDoesntExist);
			}

			ZoneInstanceEntryModel entryModel = await ZoneRepository.RetrieveAsync(zoneId)
				.ConfigureAwait(false);

			//We just return the world that this zone is for.
			return BuildSuccessfulResponseModel(new ZoneWorldConfigurationResponse((int)entryModel.WorldId));
		}

		[HttpGet("default/endpoint")]
		[ProducesJson]
		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> GetDefaultServerEndpoint()
		{
			if(!ModelState.IsValid)
				return BuildFailedResponseModel(ResolveServiceEndpointResponseCode.GeneralRequestError);

			//Small interval for race condition. So we try catch.
			try
			{
				ZoneInstanceEntryModel zone = await ZoneRepository.AnyAsync();

				//Should be good, we just send them the endpoint
				if(zone != null)
					return BuildSuccessfulResponseModel(new ZoneConnectionEndpointResponse(zone.ZoneId, new ResolvedEndpoint(zone.ZoneServerAddress, zone.ZoneServerPort)));
				else
					return BuildFailedResponseModel(ResolveServiceEndpointResponseCode.ServiceUnavailable);
			}
			catch(Exception)
			{
				//TODO: Logging/event
				return BuildFailedResponseModel(ResolveServiceEndpointResponseCode.GeneralRequestError);
			}
		}

		//TODO: Don't use ResolveServiceEndpointResponse
		//TODO: Add player authorization.
		//TODO: We should verify that the requesting user has a reason to get the server endpoint.
		[HttpGet("{id}/endpoint")]
		[ProducesJson]
		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> GetServerEndpoint([FromRoute(Name = "id")] int zoneId)
		{
			if(!ModelState.IsValid)
				return Json(new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.GeneralRequestError));

			//We reuse the service discovery response model
			if(!await ZoneRepository.ContainsAsync(zoneId))
				return Json(new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.ServiceUnlisted));

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

			return Json(new ResolveServiceEndpointResponse(ResolveServiceEndpointResponseCode.GeneralRequestError));
		}
	}
}
