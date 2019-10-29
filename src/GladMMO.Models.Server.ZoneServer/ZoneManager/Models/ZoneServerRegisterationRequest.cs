using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRegistrationRequest
	{
		//TODO: Better encapsulate this data
		/// <summary>
		/// The desired ID of the world the zone would like to register as.
		/// </summary>
		[JsonProperty]
		public int WorldId { get; private set; }

		/// <summary>
		/// The network port the zone instance will be listening for connections on.
		/// </summary>
		[JsonProperty]
		public short NetworkPort { get; private set; }

		public ZoneServerRegistrationRequest(int worldId, short networkPort)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));
			if (networkPort <= 0) throw new ArgumentOutOfRangeException(nameof(networkPort));

			WorldId = worldId;
			NetworkPort = networkPort;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private ZoneServerRegistrationRequest()
		{
			
		}
	}
}
