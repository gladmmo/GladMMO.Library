using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class CreatureContentServiceClientFactory : IFactoryCreatable<ICreatureDataServiceClient, EmptyFactoryContext>
	{
		public ICreatureDataServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<ICreatureDataServiceClient>("http://test-guardians-contentserver.azurewebsites.net/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(null) });
		}
	}
}
