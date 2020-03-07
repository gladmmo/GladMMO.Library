using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRegistrationResponse
	{
		//TODO: Should the zone registry service indicate anything to the zone?
		/// <summary>
		/// The id of the registered zone.
		/// </summary>
		[JsonProperty]
		public int ZoneId { get; private set; }

		public ZoneServerRegistrationResponse(int zoneId)
		{
			if (zoneId <= 0) throw new ArgumentOutOfRangeException(nameof(zoneId));

			ZoneId = zoneId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public ZoneServerRegistrationResponse()
		{
			
		}
	}
}
