using System;
using System.Collections.Generic;
using System.Text;
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
		}
	}
}
