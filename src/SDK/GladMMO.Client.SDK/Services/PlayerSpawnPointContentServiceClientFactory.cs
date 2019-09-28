using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class PlayerSpawnPointContentServiceClientFactory : IFactoryCreatable<IPlayerSpawnPointDataServiceClient, EmptyFactoryContext>
	{
		public IPlayerSpawnPointDataServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<IPlayerSpawnPointDataServiceClient>("http://72.190.177.214:5005/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
