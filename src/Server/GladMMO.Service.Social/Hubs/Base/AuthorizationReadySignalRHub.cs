using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Base <see cref="Hub"/> for SignalR hubs that have claims authorization support.
	/// </summary>
	public abstract class AuthorizationReadySignalRHub : BaseSignalRHub
	{
		/// <summary>
		/// The reader for the claims.
		/// </summary>
		protected IClaimsPrincipalReader ClaimsReader { get; }

		/// <inheritdoc />
		protected AuthorizationReadySignalRHub([FromServices] IClaimsPrincipalReader claimsReader, [FromServices] ILogger<AuthorizationReadySignalRHub> logger)
			: base(logger)
		{
			if(claimsReader == null) throw new ArgumentNullException(nameof(claimsReader));

			ClaimsReader = claimsReader;
		}
	}

	/// <summary>
	/// Base <see cref="Hub"/> for SignalR hubs that have claims authorization support.
	/// </summary>
	public abstract class AuthorizationReadySignalRHub<T> : BaseSignalRHub<T> 
		where T : class
	{
		/// <summary>
		/// The reader for the claims.
		/// </summary>
		protected IClaimsPrincipalReader ClaimsReader { get; }

		/// <inheritdoc />
		protected AuthorizationReadySignalRHub([FromServices] IClaimsPrincipalReader claimsReader, [FromServices] ILogger<AuthorizationReadySignalRHub<T>> logger)
			: base(logger)
		{
			if(claimsReader == null) throw new ArgumentNullException(nameof(claimsReader));

			ClaimsReader = claimsReader;
		}
	}
}
