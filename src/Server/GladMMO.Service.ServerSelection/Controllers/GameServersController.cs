using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class GameServersController : Controller
	{
		/// <summary>
		/// Controller logger.
		/// </summary>
		private ILogger<GameServersController> Logger { get; }

		/// <inheritdoc />
		public GameServersController([FromServices] ILogger<GameServersController> logger)
		{
			if(logger == null) throw new ArgumentNullException(nameof(logger), $"Failed to provider logging instance to controller.");

			Logger = logger;
		}

		//We don't need to authorize the users here
		//Users don't need to be logged in to know the gameservers since it's
		//not a secret and it is not unique to the users.
		[ResponseCache(Duration = 15, Location = ResponseCacheLocation.Any)] //we want to cache this for 15 seconds.
		[HttpGet("all")]
		public async Task<JsonResult> GetAllServers([FromServices] IGameServersStoreRepository gameserverRepository)
		{
			if(Logger.IsEnabled(LogLevel.Debug))
				Logger.LogDebug($"User requesting game servers: {HttpContext.Connection.RemoteIpAddress}:{HttpContext.Connection.RemotePort} .");

			GameServerEntry[] entries = await gameserverRepository.RetriveServers();

			return Json(new GameServerListResponseModel(entries));
		}
	}
}
