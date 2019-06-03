using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PlayFab.Internal;
using PlayFab.ServerModels;

namespace GladMMO
{
	[JsonObject]
	public class GladMMOPlayFabLoginResult
	{
		/// <summary>
		/// Unique token authorizing the user and game at the server level, for the current session.
		/// </summary>
		[JsonProperty]
		public string SessionTicket { get; private set; }

		public GladMMOPlayFabLoginResult(string sessionTicket)
		{
			if (string.IsNullOrWhiteSpace(sessionTicket))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(sessionTicket));

			SessionTicket = sessionTicket;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public GladMMOPlayFabLoginResult()
		{
			
		}
	}
}
