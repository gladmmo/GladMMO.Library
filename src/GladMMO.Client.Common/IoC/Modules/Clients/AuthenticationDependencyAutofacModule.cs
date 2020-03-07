using System; using FreecraftCore;
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

		//We mostly expose this again for WebGL since calling sync tasks normally is a bad idea for WebGL in Unity3D.
		[JetBrains.Annotations.CanBeNull]
		public static string PrecomputedEndpoint { get; set; }

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IAuthenticationService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				if (PrecomputedEndpoint != null)
					return RestService.For<IAuthenticationService>(PrecomputedEndpoint, new RefitSettings() {HttpMessageHandlerFactory = HttpClientHandlerFactory});
				else
					return new AsyncEndpointAuthenticationService(QueryForRemoteServiceEndpoint(serviceDiscovery, "Authentication"), new RefitSettings() { HttpMessageHandlerFactory = HttpClientHandlerFactory });
			});
		}
	}
}
