using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Refit;

namespace GladMMO
{
	//TODO: Consolidate with client version.
	public sealed class ContentServerDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IDownloadableContentServerServiceClient>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				return new AsyncEndpointDownloadableContentService(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
			});

			builder.Register<ICreatureDataServiceClient>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				//IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				//return new AsyncCreatureDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
				return new AsyncCreatureDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"));
			});

			builder.Register<IGameObjectDataServiceClient>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				//IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				//return new AsyncCreatureDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
				return new AsyncGameObjectDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"));
			});

			builder.Register<IPlayerSpawnPointDataServiceClient>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				//IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				//return new AsyncCreatureDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
				return new AsyncPlayerSpawnPointDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"));
			});

			builder.Register<IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel>>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				//IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				//return new AsyncCreatureDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
				return new AsyncGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel>(CreateBehaviourDataEndpointFromServiceEndpoint(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), "WorldTeleporterData"));
			});

			builder.Register<IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel>>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				//IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				//return new AsyncCreatureDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
				return new AsyncGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel>(CreateBehaviourDataEndpointFromServiceEndpoint(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), "AvatarPedestalData"));
			});
		}

		private async Task<string> CreateBehaviourDataEndpointFromServiceEndpoint(Task<string> endpoint, string behaviourNameType)
		{
			string endpointString = await endpoint;

			return $"{endpointString}api/{behaviourNameType}/";
		}
	}
}
