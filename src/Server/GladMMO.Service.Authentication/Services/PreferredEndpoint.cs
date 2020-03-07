using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class PreferredEndpoint
	{
		public string Endpoint { get; }

		public short Port { get; }

		public PreferredEndpoint([JetBrains.Annotations.NotNull] string endpoint, short port)
		{
			if (string.IsNullOrEmpty(endpoint))
				throw new ArgumentException("Value cannot be null or empty.", nameof(endpoint));

			Endpoint = endpoint;
			Port = port;
		}
	}
}
