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
	}
}
