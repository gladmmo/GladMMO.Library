using System; using FreecraftCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Loads Endpoint{Region}.json files from the Endpoints directory.
	/// This directory should be relative to the working directory of the launched application.
	/// </summary>
	public sealed class FilestoreBasedRegionNamedEndpointStoreRepository : IRegionNamedEndpointStoreRepository, IRegionbasedNameEndpointResolutionRepository
	{
		private IRegionalServiceFilePathBuilder PathBuilder { get; }

		public FilestoreBasedRegionNamedEndpointStoreRepository(IRegionalServiceFilePathBuilder pathBuilder)
		{
			if(pathBuilder == null) throw new ArgumentNullException(nameof(pathBuilder));

			PathBuilder = pathBuilder;
		}

		/// <inheritdoc />
		public async Task<NameEndpointResolutionStorageModel> Retrieve(ClientRegionLocale region)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");

			using (StreamReader reader = File.OpenText(BuildRegionEndpointFileLocation(region)))
			{
				return Newtonsoft.Json.JsonConvert.DeserializeObject<NameEndpointResolutionStorageModel>(await reader.ReadToEndAsync());
			}
		}

		/// <inheritdoc />
		public Task<bool> HasRegionStore(ClientRegionLocale region)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");

			return Task.FromResult(File.Exists(BuildRegionEndpointFileLocation(region)));
		}

		private string BuildRegionEndpointFileLocation(ClientRegionLocale region)
		{
			return PathBuilder.BuildPath(region);
		}

		/// <inheritdoc />
		public async Task<ResolvedEndpoint> RetrieveAsync(ClientRegionLocale locale, string serviceType)
		{
			if(!await HasEntryAsync(locale, serviceType))
				throw new InvalidOperationException($"Does not contain informations for Region: {locale} Type: {serviceType}");

			NameEndpointResolutionStorageModel regionInfo = await Retrieve(locale);

			return new ResolvedEndpoint(regionInfo.ServiceEndpoints[serviceType].Address, regionInfo.ServiceEndpoints[serviceType].Port);
		}

		/// <inheritdoc />
		public Task<bool> HasDataForRegionAsync(ClientRegionLocale locale)
		{
			return HasRegionStore(locale);
		}

		/// <inheritdoc />
		public async Task<bool> HasEntryAsync(ClientRegionLocale locale, string serviceType)
		{
			if(!await HasDataForRegionAsync(locale))
				return false;

			NameEndpointResolutionStorageModel regionInfo = await Retrieve(locale);

			if(regionInfo.ServiceEndpoints.ContainsKey(serviceType))
				return true;

			return false;
		}
	}
}
