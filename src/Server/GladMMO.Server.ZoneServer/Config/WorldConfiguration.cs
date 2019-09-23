using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GladMMO
{
	[JsonObject]
	public class WorldConfiguration
	{
		[JsonProperty]
		public long WorldId { get; private set; }

		public WorldConfiguration(long worldId)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));
			WorldId = worldId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public WorldConfiguration()
		{
			
		}
	}
}
