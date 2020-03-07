using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseContextBasedRegionBasedNameEndpointResolutionRepository : IRegionbasedNameEndpointResolutionRepository
	{
		/// <summary>
		/// DBContext containing the endpoints.
		/// </summary>
		public NamedEndpointDbContext EndpointsContext { get; }

		public DatabaseContextBasedRegionBasedNameEndpointResolutionRepository([FromServices] NamedEndpointDbContext endpointsContext)
		{
			if (endpointsContext == null) throw new ArgumentNullException(nameof(endpointsContext));

			EndpointsContext = endpointsContext;
		}

		/// <inheritdoc />
		public async Task<ResolvedEndpoint> RetrieveAsync(ClientRegionLocale locale, string serviceType)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), locale)) throw new ArgumentOutOfRangeException(nameof(locale), "Value should be defined in the ClientRegionLocale enum.");
			if (string.IsNullOrWhiteSpace(serviceType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceType));

			NamedResolvedEndpointEntryModel model = await EndpointsContext.Endpoints.FirstOrDefaultAsync(e => e.Region == locale && e.Service == serviceType);

			if(model == null)
				throw new KeyNotFoundException($"Provided keypair {locale} and {serviceType} not found.");

			return new ResolvedEndpoint(model.EndpointAddress, model.EndpointPort);
		}

		/// <inheritdoc />
		public Task<bool> HasDataForRegionAsync(ClientRegionLocale locale)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), locale)) throw new ArgumentOutOfRangeException(nameof(locale), "Value should be defined in the ClientRegionLocale enum.");

			return EndpointsContext.Endpoints.AnyAsync(e => e.Region == locale);
		}

		/// <inheritdoc />
		public Task<bool> HasEntryAsync(ClientRegionLocale locale, string serviceType)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), locale)) throw new ArgumentOutOfRangeException(nameof(locale), "Value should be defined in the ClientRegionLocale enum.");
			if (string.IsNullOrWhiteSpace(serviceType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceType));

			return EndpointsContext.Endpoints.AnyAsync(e => e.Region == locale && e.Service == serviceType);
		}
	}
}
