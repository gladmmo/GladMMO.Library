using System; using FreecraftCore;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GladMMO
{
	[Owned]
	[JsonObject]
	public sealed class ResolvedEndpoint
	{
		[JsonProperty(Required = Required.Always, PropertyName = "EndpointAddress")] //JSON prop names here for backwards compat
		public string Address { get; internal set; }

		[JsonProperty(Required = Required.Always, PropertyName = "EndpointPort")] //JSON prop names here for backwards compat
		public int Port { get; internal set; }

		public ResolvedEndpoint(string endpointAddress, int endpointPort)
		{
			if(string.IsNullOrWhiteSpace(endpointAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpointAddress));
			if(endpointPort <= 0 || endpointPort >= 65535) throw new ArgumentOutOfRangeException(nameof(endpointPort));

			Address = endpointAddress;
			Port = endpointPort;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		private ResolvedEndpoint()
		{

		}

		public override string ToString()
		{
			return $"Address: {Address} Port: {Port}";
		}
	}
}