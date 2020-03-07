using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Request that resolves a specific service's endpoint.
	/// Many services do not and should not have static endpoints. This also allows for the ability to
	/// naively loadbalance, though it's unlikely that it will be done this way, or direct users by region.
	/// </summary>
	[JsonObject]
	public sealed class ResolveServiceEndpointRequest
	{
		/// <summary>
		/// Indicates the region of the client requesting.
		/// This parameter can be used to connect clients to closer services. For example
		/// there may be a voice chat service and connecting them to a closer endpoint in their country
		/// may serve better quality.
		/// </summary>
		[JsonProperty]
		public ClientRegionLocale Region { get; private set; }

		/// <summary>
		/// Indicates the service requested for resolution.
		/// </summary>
		[JsonProperty]
		public string ServiceType { get; private set; }

		public ResolveServiceEndpointRequest(ClientRegionLocale region, string serviceType)
		{
			if(!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");
			if(string.IsNullOrWhiteSpace(serviceType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceType));

			Region = region;
			ServiceType = serviceType;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected ResolveServiceEndpointRequest()
		{

		}
	}
}