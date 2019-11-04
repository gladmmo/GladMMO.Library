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
			return Refit.RestService.For<IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel>>("http://test-guardians-contentserver.azurewebsites.net/api/AvatarPedestalData/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
