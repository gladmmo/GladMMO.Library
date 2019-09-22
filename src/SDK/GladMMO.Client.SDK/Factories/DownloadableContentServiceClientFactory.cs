using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO
{
	public sealed class DownloadableContentServiceClientFactory : IFactoryCreatable<IDownloadableContentServerServiceClient, EmptyFactoryContext>
	{
		public IDownloadableContentServerServiceClient Create(EmptyFactoryContext context)
		{
			return Refit.RestService.For<IDownloadableContentServerServiceClient>("http://72.190.177.214:5005/", new RefitSettings() {HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository())});
		}
	}
}
