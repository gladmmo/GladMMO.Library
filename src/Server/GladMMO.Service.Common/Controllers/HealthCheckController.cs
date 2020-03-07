using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// General health check controller that exposes a GET action at
	/// /api/healthcheck that returns a status code of 200 if everything is
	/// healthy.
	/// </summary>
	[Route("api/[controller]")]
	public sealed class HealthCheckController : Controller
	{
		private ILogger<HealthCheckController> Logger { get; }

		/// <inheritdoc />
		public HealthCheckController(ILogger<HealthCheckController> logger)
		{
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			Logger = logger;
		}

		/// <summary>
		/// Returns an empty response with status code Ok (200).
		/// </summary>
		/// <returns>Returns </returns>
		[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)] //disable caching
		[HttpGet]
		public IActionResult Check()
		{
			if(Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"Recieved health check from: {HttpContext.Connection.RemoteIpAddress}:{HttpContext.Connection.RemotePort}");

			//TODO: Should we make some determinations other than this? Maybe if we have too many ongoing requests?
			return Ok();
		}
	}
}
