using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// The base controller Type for guardians SignalR <see cref="Hub"/>
	/// </summary>
	public abstract class BaseSignalRHub : Hub
	{
		/// <summary>
		/// The logging service for the Hub.
		/// </summary>
		protected ILogger<BaseSignalRHub> Logger { get; }

		/// <inheritdoc />
		protected BaseSignalRHub([FromServices] ILogger<BaseSignalRHub> logger)
		{
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			Logger = logger;
		}
	}

	/// <summary>
	/// The base controller Type for guardians SignalR <see cref="Hub"/>
	/// </summary>
	public abstract class BaseSignalRHub<T> : Hub<T> 
		where T : class
	{
		/// <summary>
		/// The logging service for the Hub.
		/// </summary>
		protected ILogger<BaseSignalRHub<T>> Logger { get; }

		/// <inheritdoc />
		protected BaseSignalRHub([FromServices] ILogger<BaseSignalRHub<T>> logger)
		{
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			Logger = logger;
		}
	}
}
