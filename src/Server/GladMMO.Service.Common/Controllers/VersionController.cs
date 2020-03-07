using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Controller that exposes a gettable version identifier over GET.
	/// </summary>
	[Route("api/[controller]")]
	public sealed class VersionController : Controller
	{
		private ILogger<VersionController> Logger { get; }

		/// <inheritdoc />
		public VersionController([JetBrains.Annotations.NotNull] ILogger<HealthCheckController> logger)
		{
			if (logger == null) throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		/// Returns an empty response with status code Ok (200).
		/// </summary>
		/// <returns>Returns </returns>
		[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)] //disable caching
		[HttpGet]
		public IActionResult GetVersion()
		{
			return Ok("0.0.2");
		}
	}
}
