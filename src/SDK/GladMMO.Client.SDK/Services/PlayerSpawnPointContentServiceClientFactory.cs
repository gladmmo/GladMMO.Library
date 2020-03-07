using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class PlayerSpawnPointContentServiceClientFactory : IFactoryCreatable<IPlayerSpawnPointDataServiceClient, EmptyFactoryContext>
	{
		public IPlayerSpawnPointDataServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<IPlayerSpawnPointDataServiceClient>("http://test-guardians-contentserver.azurewebsites.net/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
