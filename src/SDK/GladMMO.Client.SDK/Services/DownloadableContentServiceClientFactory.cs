using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO
{
	public sealed class DownloadableContentServiceClientFactory : IFactoryCreatable<IDownloadableContentServerServiceClient, EmptyFactoryContext>
	{
		public IDownloadableContentServerServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<IDownloadableContentServerServiceClient>("http://test-guardians-contentserver.azurewebsites.net/", new RefitSettings() {HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository())});
		}
	}
}
