using System; using FreecraftCore;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ResolvedEndpoint
	{
		[JsonProperty(Required = Required.Always)]
		public string EndpointAddress { get; }

		[JsonProperty(Required = Required.Always)]
		public int EndpointPort { get; }

		public ResolvedEndpoint(string endpointAddress, int endpointPort)
		{
			if(string.IsNullOrWhiteSpace(endpointAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpointAddress));
			if(endpointPort <= 0 || endpointPort >= 65535) throw new ArgumentOutOfRangeException(nameof(endpointPort));

			EndpointAddress = endpointAddress;
			EndpointPort = endpointPort;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		private ResolvedEndpoint()
		{

		}

		public override string ToString()
		{
			return $"Address: {EndpointAddress} Port: {EndpointPort}";
		}
	}
}