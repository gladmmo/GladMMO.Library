using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public class NetworkConfiguration
	{
		[JsonRequired]
		[JsonProperty]
		public short Port { get; private set; }

		[JsonRequired]
		[JsonProperty]
		public string ListenerAddress { get; private set; }

		public NetworkConfiguration(short port, [NotNull] string listenerAddress)
		{
			if (port <= 0) throw new ArgumentOutOfRangeException(nameof(port));
			if(string.IsNullOrWhiteSpace(listenerAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(listenerAddress));

			Port = port;
			ListenerAddress = listenerAddress;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public NetworkConfiguration()
		{
			
		}
	}
}
