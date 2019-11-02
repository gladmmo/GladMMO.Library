using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class SocialServiceDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<ISocialService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				return new AsyncSocialServiceClient(QueryForRemoteServiceEndpoint(serviceDiscovery, GladMMONetworkConstants.SOCIAL_SERVICE_NAME), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
			});

			builder.RegisterType<DefaultRemoteSocialHubServer>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<DefaultRemoteSocialHubClient>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
