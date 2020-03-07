using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class AzureStaticEndpointRepository : IRegionbasedNameEndpointResolutionRepository
	{
		public async Task<ResolvedEndpoint> RetrieveAsync(ClientRegionLocale locale, string serviceType)
		{
			//This is kinda hacky but I named the authentication app service before thinking about this properly
			//so now we have to have this ugly check
			if (serviceType.ToLower() == "authentication")
				serviceType = "auth";

#if AZURE_DEBUG
			string deploymentName = "test";
#elif AZURE_RELEASE
			string deploymentName = "prod";
#else
			string deploymentName = null;
			throw new NotImplementedException($"Unsupported Build Configuration detected for {nameof(AzureStaticEndpointRepository)}");
#endif

			//https://test-guardians-auth.azurewebsites.net
			return new ResolvedEndpoint($"http://{deploymentName}-Guardians-{serviceType}.azurewebsites.net", 80);
		}

		public async Task<bool> HasDataForRegionAsync(ClientRegionLocale locale)
		{
			//Just pretend.
			return true;
		}

		public async Task<bool> HasEntryAsync(ClientRegionLocale locale, string serviceType)
		{
			//Just pretend.
			return true;
		}
	}
}
