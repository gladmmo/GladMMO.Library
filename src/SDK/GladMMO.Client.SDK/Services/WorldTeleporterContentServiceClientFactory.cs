using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class WorldTeleporterContentServiceClientFactory : IFactoryCreatable<IWorldTeleporterDataServiceClient, EmptyFactoryContext>
	{
		public IWorldTeleporterDataServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<IWorldTeleporterDataServiceClient>("http://72.190.177.214:5005/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
