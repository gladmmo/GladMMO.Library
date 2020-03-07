using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Consul.Net;

namespace GladMMO
{
	public sealed class ConsulRegionNamedEndpointStoreRepository : IRegionbasedNameEndpointResolutionRepository
	{
		private IConsulClient<IConsulCatalogServiceHttpApiService> Client { get; }

		//TODO: Handle better caching
		private Dictionary<string, ResolvedEndpoint> EntryCache { get; }

		/// <inheritdoc />
		public ConsulRegionNamedEndpointStoreRepository(IConsulClient<IConsulCatalogServiceHttpApiService> client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			Client = client;
			EntryCache = new Dictionary<string, ResolvedEndpoint>(2);
		}

		/// <inheritdoc />
		public async Task<ResolvedEndpoint> RetrieveAsync(ClientRegionLocale locale, string serviceType)
		{
			//TODO: Validate string so they don't sniff out private services
			if(!Enum.IsDefined(typeof(ClientRegionLocale), locale)) throw new ArgumentOutOfRangeException(nameof(locale), "Value should be defined in the ClientRegionLocale enum.");
			if(string.IsNullOrWhiteSpace(serviceType)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceType));

			if(EntryCache.ContainsKey(ConvertLocaleServiceTypeToString(locale, serviceType)))
				return EntryCache[ConvertLocaleServiceTypeToString(locale, serviceType)];

			CatalogServiceNodeEntry[] result = await QueryConsul(locale, serviceType);

			if(!QueryResultIsValid(result))
				ThrowQueryException(locale, serviceType);

			return ParseQueryToEndpoint(locale, serviceType, result);
		}

		private static ResolvedEndpoint ParseQueryToEndpoint(ClientRegionLocale locale, string serviceType, CatalogServiceNodeEntry[] result)
		{
			if(!QueryResultIsValid(result))
				ThrowQueryException(locale, serviceType);

			//TODO: Check health
			//TODO: Manual loadbalancing roadrobin?
			CatalogServiceNodeEntry entry = result.First();

			return ConvertConsulEntryToEndpoint(entry);
		}

		private static void ThrowQueryException(ClientRegionLocale locale, string serviceType)
		{
			throw new InvalidOperationException($"Failed to query Consul for Locale:{locale} ServiceType: {serviceType}");
		}

		private static ResolvedEndpoint ConvertConsulEntryToEndpoint(CatalogServiceNodeEntry entry)
		{
			//We need to use ServiceAddress and not Address. Address is the Consul address.
			return new ResolvedEndpoint(entry.ServiceAddress, entry.ServicePort);
		}

		private static bool QueryResultIsValid(CatalogServiceNodeEntry[] result)
		{
			if(result == null)
				return false;

			if(result.Length < 1)
				return false;

			return true;
		}

		private Task<CatalogServiceNodeEntry[]> QueryConsul(ClientRegionLocale locale, string serviceType)
		{
			return Client.Service.GetServiceNodes(serviceType, locale.ToString());
		}

		/// <inheritdoc />
		public Task<bool> HasDataForRegionAsync(ClientRegionLocale locale)
		{
			//TODO: How should we handle region stuff with consul?
			return Task.FromResult(true);
		}

		/// <inheritdoc />
		public async Task<bool> HasEntryAsync(ClientRegionLocale locale, string serviceType)
		{
			if(EntryCache.ContainsKey(ConvertLocaleServiceTypeToString(locale, serviceType)))
				return true;

			CatalogServiceNodeEntry[] result = await QueryConsul(locale, serviceType);

			bool valid = QueryResultIsValid(result);

			if(valid)
				//Add to cache so we can look up from the cache in this lifetime of the service
				EntryCache.Add(ConvertLocaleServiceTypeToString(locale, serviceType), ParseQueryToEndpoint(locale, serviceType, result));

			return valid;
		}

		private static string ConvertLocaleServiceTypeToString(ClientRegionLocale locale, string serviceType)
		{
			return $"{serviceType}:{locale}";
		}
	}
}
