using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneConnectionEndpointResponse
	{
		[JsonProperty]
		public ResolvedEndpoint Endpoint { get; private set; }

		[JsonProperty]
		public int ZoneId { get; private set; }

		public ZoneConnectionEndpointResponse(int zoneId, [JetBrains.Annotations.NotNull] ResolvedEndpoint endpoint)
		{
			if (zoneId <= 0) throw new ArgumentOutOfRangeException(nameof(zoneId));
			Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
			ZoneId = zoneId;
		}

		[JsonConstructor]
		private ZoneConnectionEndpointResponse()
		{
			
		}
	}
}
