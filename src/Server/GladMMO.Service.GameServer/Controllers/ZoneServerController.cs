using Microsoft.AspNetCore.Mvc;
using System;
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

		[ProducesJson]
		[HttpPost("register")]
		[NoResponseCache]
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)] //TODO: Eventually we'll need to auth these zoneservers.
		public async Task<IActionResult> RegisterZoneServer([FromBody] ZoneServerRegisterationRequest registerationRequest, 
			[FromServices] IZoneInstanceWorkQueue instanceWorkQueue, [FromServices] IZoneServerRepository zoneRepo)
		{
			//TODO: JSON
			if(!ModelState.IsValid)
				return BadRequest();

			//Conceptually, just because there is no work doesn't mean this is an error
			//Requesting users who were trying to make an instance could have abandoned that request.
			//Instances may eventually free themselves after inactivity and attempt to reregister instead of shutting down at first
			//So we just want to say "Nothing to do right now" so they can sleep and maybe manually shutdown after a timeout period.
			if(instanceWorkQueue.isEmpty)
			{
				return NoWorkForInstanceResponse();
			}

			//The specification says this could complete immediately
			//with null if no works exists, there is technically a data race condition between checking isEmpty
			//and trying to dequeue so the result may not be predictible.
			ZoneInstanceWorkEntry zoneInstanceWorkEntry = await instanceWorkQueue.DequeueAsync()
				.ConfigureAwait(false);

			//TODO: If anything here fails after dequeueing we could lose CRITICAL data to keep things running
			//We need VERY good failure handling, and to reenter this work request into the queue somehow.
			//Otherwise the request for the instance will be lost and unhandled forever.
			ProjectVersionStage.AssertAlpha();

			if(zoneInstanceWorkEntry == null)
			{
				return NoWorkForInstanceResponse();
			}

			//TODO: Validate endpoint
			//Since there IS work to do, we can't just tell the zone instance
			//We must register it into the zone server repo
			if(!await zoneRepo.TryCreateAsync(new ZoneInstanceEntryModel(registerationRequest.ZoneServerEndpoint.EndpointAddress, (short)registerationRequest.ZoneServerEndpoint.EndpointPort, zoneInstanceWorkEntry.WorldId)))
			{
				//As stated above, we need good handling for this else
				//we will encounter MAJOR issues.
				return NoWorkForInstanceResponse();
			}

			//Success
			return Ok(new ZoneServerRegisterationResponse(zoneInstanceWorkEntry.WorldId));
		}

		private IActionResult NoWorkForInstanceResponse()
		{
			//TODO: We should tell the zoneserver there is nothing for him to do.
			return Ok(new ZoneServerRegisterationResponse(ZoneServerRegisterationResponseCode.NoWorkTodo));
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
