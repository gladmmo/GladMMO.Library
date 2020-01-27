using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GladNet;
using Refit;

namespace GladMMO
{
	public sealed class AuthenticationDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		//A design smell, but we make this static so that this can be controlled by consumers of the
		//GladMMO library without massively complicating the dependency graph of the root module registering.
		public static Func<HttpClientHandler> HttpClientHandlerFactory { get; set; } = () => new FiddlerEnabledWebProxyHandler();

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IAuthenticationService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				return new AsyncEndpointAuthenticationService(QueryForRemoteServiceEndpoint(serviceDiscovery, "Authentication"), new RefitSettings() { HttpMessageHandlerFactory = HttpClientHandlerFactory });
			});
		}
	}
}
