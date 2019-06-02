using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using UnityEngine;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class ZoneServerDataController : Controller
	{
		private ILogger<ZoneServerDataController> Logger { get; }

		/// <inheritdoc />
		public ZoneServerDataController([NotNull] ILogger<ZoneServerDataController> logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//TODO: We should add ZoneServer authorization so that users can't query waypoints
		[HttpGet("waypoint/{id}")]
		[ProducesJson]
		[ResponseCache(Duration = int.MaxValue)]
		public async Task<IActionResult> GetPathWaypoints([FromRoute(Name = "id")] int pathId, [FromServices] IWaypointsRepository waypointsRepository)
		{
			if(!ModelState.IsValid)
				return Ok(new ZoneServerWaypointQueryResponse(WaypointQueryResponseCode.GeneralServerError));

			if(!await waypointsRepository.ContainsPathAsync(pathId)
				.ConfigureAwait(false))
			{
				Logger.LogWarning($"Requested PathId: {pathId} but was not found in {nameof(IWaypointsRepository)}");
				return Ok(new ZoneServerWaypointQueryResponse(WaypointQueryResponseCode.EntryNotFound));
			}

			var result = await waypointsRepository.RetrievePointsFromPathAsync(pathId)
				.ConfigureAwait(false);

			Vector3[] vectorArray = result
				.OrderBy(model => model.PointId)
				.Select(p => new Vector3(p.Point.X, p.Point.Y, p.Point.Z))
				.ToArrayTryAvoidCopy();

			return Accepted(new ZoneServerWaypointQueryResponse(vectorArray));
		}
	}
}
