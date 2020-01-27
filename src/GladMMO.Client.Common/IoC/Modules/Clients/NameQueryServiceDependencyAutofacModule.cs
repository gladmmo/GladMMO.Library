using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class NameQueryServiceDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<INameQueryService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				//TODO: Eventually gameserver won't be endpoint for namequeries.
				return new AsyncEndpointNameQueryService(QueryForRemoteServiceEndpoint(serviceDiscovery, "NameQuery"), new RefitSettings() { HttpMessageHandlerFactory = () => new FiddlerEnabledWebProxyHandler() });
			});
		}
	}
}
