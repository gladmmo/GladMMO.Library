using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class ZoneClientDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IZoneAuthenticationService>(context =>
				{
					IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
					IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

					return new AsyncZoneAuthenticationServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, "ZoneAuthentication"), new RefitSettings() {HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository)});
				})
				.SingleInstance();

			builder.Register<IZoneRegistryService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				return new AsyncEndpointZoneRegistryService(QueryForRemoteServiceEndpoint(serviceDiscovery, "ZoneManager"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
			})
				//.As<IZoneRegistryServiceQueueable>()
				.As<IZoneRegistryService>()
				.SingleInstance();
		}
	}
}
