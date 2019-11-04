using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.SDK
{
	public sealed class WorldTeleporterContentServiceClientFactory : GenericGameObjectBehaviourServiceClientFactory<WorldTeleporterInstanceModel>
	{
		public override IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel> Create(EmptyFactoryContext context)
		{
			//api/WorldTeleporterData
			return Refit.RestService.For<IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel>>("http://test-guardians-contentserver.azurewebsites.net/api/WorldTeleporterData/", new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(new ContentSDKAuthenticationTokenRepository()) });
		}
	}
}
