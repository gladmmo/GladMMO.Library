using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneWorldConfigurationResponse
	{
		/// <summary>
		/// The WorldId associated with the zone.
		/// </summary>
		[JsonProperty]
		public int WorldId { get; private set; }

		//We may have more stuff here. Such as name
		//or instance owner or if there are like configurations
		//such as speed or who knows what.

		public ZoneWorldConfigurationResponse(int worldId)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			WorldId = worldId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private ZoneWorldConfigurationResponse()
		{
			
		}
	}
}
