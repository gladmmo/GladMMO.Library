using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GladMMO
{
	//TODO: Should we move this into HaloLive.Library?
	/// <summary>
	/// Data model that represented a named resolved endpoint.
	/// This is the data model used to represent the endpoints in a database/table format.
	/// </summary>
	public sealed class NamedResolvedEndpointEntryModel
	{
		/// <summary>
		/// Indicates the region of the endpoint.
		/// </summary>
		[Required]
		public ClientRegionLocale Region { get; private set; } //must have setter for EF

		/// <summary>
		/// Indicates the service.
		/// </summary>
		[Required]
		public string Service { get; private set; } //must have setter for EF

		/// <summary>
		/// Indicates the endpoint address.
		/// </summary>
		[Required]
		public string EndpointAddress { get; private set; } //must have setter for EF

		/// <summary>
		/// Indicates the endpoint's port.
		/// </summary>
		[Required]
		public int EndpointPort { get; private set; } //must have setter for EF

		public NamedResolvedEndpointEntryModel(ClientRegionLocale region, string service, string endpointAddress, int endpointPort)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");
			if (string.IsNullOrWhiteSpace(endpointAddress)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpointAddress));
			if (string.IsNullOrWhiteSpace(service)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(service));

			Region = region;
			Service = service;
			EndpointAddress = endpointAddress;
			EndpointPort = endpointPort;
		}

		/// <summary>
		/// Ef required constructor.
		/// </summary>
		public NamedResolvedEndpointEntryModel()
		{
			
		}
	}
}
