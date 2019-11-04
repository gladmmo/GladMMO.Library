using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class GameObjectContentServiceClientFactory : IFactoryCreatable<IGameObjectDataServiceClient, EmptyFactoryContext>
	{
		public IGameObjectDataServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<IGameObjectDataServiceClient>("http://test-guardians-contentserver.azurewebsites.net/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
