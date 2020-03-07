using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using GladMMO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GladMMO
{
	/// <summary>
	/// Format for the endpoint map storage.
	/// </summary>
	[JsonObject]
	public sealed class NameEndpointResolutionStorageModel
	{
		/// <summary>
		/// The region these endpoints are for.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public ClientRegionLocale Region { get; }

		/// <summary>
		/// The Dictionary of services and endpoints.
		/// </summary>
		[JsonProperty(nameof(ServiceEndpoints), Required = Required.Default)]
		private Dictionary<string, ResolvedEndpoint> _ServiceEndpoints { get; }

		/// <summary>
		/// The Dictionary of services and endpoints
		/// </summary>
		[JsonIgnore]
		public IReadOnlyDictionary<string, ResolvedEndpoint> ServiceEndpoints => _ServiceEndpoints;

		//We don't need a real ctor because we load a file
		public NameEndpointResolutionStorageModel(ClientRegionLocale region, Dictionary<string, ResolvedEndpoint> serviceEndpoints)
		{
			if(serviceEndpoints == null) throw new ArgumentNullException(nameof(serviceEndpoints));
			if(!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");

			Region = region;
			_ServiceEndpoints = serviceEndpoints;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected NameEndpointResolutionStorageModel()
		{

		}
	}
}