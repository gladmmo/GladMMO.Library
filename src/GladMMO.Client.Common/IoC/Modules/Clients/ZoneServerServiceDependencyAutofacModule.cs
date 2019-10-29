using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class ZoneServerServiceDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IZoneDataService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				return new AsyncEndpointZoneDataService(QueryForRemoteServiceEndpoint(serviceDiscovery, "GameServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new FiddlerEnabledWebProxyHandler() });
			});
		}
	}
}
