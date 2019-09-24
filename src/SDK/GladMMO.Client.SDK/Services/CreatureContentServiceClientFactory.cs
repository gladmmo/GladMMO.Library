using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class CreatureContentServiceClientFactory : IFactoryCreatable<ICreatureDataServiceClient, EmptyFactoryContext>
	{
		public ICreatureDataServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<ICreatureDataServiceClient>("http://72.190.177.214:5005/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
