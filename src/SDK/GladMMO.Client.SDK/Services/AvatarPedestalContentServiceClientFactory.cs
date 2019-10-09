using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class AvatarPedestalContentServiceClientFactory : GenericGameObjectBehaviourServiceClientFactory<AvatarPedestalInstanceModel>
	{
		public override IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel> Create(EmptyFactoryContext context)
		{
			//api/AvatarPedestalData
			return Refit.RestService.For<IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel>>("http://72.190.177.214:5005/api/AvatarPedestalData/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
