using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class ContentServerDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IContentServerServiceClient>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				return new AsyncEndpointContentServerService(QueryForRemoteServiceEndpoint(serviceDiscovery, "ContentServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new FiddlerEnabledWebProxyHandler() });
			});
		}
	}
}
