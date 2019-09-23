using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRootConfiguration
	{
		/// <summary>
		/// Optional world configuration field.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public WorldConfiguration WorldConfig { get; private set; }

		[JsonProperty(Required = Required.Default)]
		public NetworkConfiguration NetworkConfig { get; private set;  }

		public ZoneServerRootConfiguration([NotNull] WorldConfiguration worldConfig, [NotNull] NetworkConfiguration networkConfig)
		{
			WorldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));
			NetworkConfig = networkConfig ?? throw new ArgumentNullException(nameof(networkConfig));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public ZoneServerRootConfiguration()
		{
			
		}
	}
}
