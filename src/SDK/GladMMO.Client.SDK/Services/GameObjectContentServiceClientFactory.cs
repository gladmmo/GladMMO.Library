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
			return Refit.RestService.For<IGameObjectDataServiceClient>("http://72.190.177.214:5005/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
