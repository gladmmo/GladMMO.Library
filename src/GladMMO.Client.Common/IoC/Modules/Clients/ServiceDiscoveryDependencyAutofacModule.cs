using System; using FreecraftCore;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class ServiceDiscoveryDependencyAutofacModule : Module
	{
		//A design smell, but we make this static so that this can be controlled by consumers of the
		//GladMMO library without massively complicating the dependency graph of the root module registering.
		public static Func<HttpClientHandler> HttpClientHandlerFactory { get; set; } = () => new FiddlerEnabledWebProxyHandler();

		private string ServiceDiscoveryUrl { get; }

		public ServiceDiscoveryDependencyAutofacModule([NotNull] string serviceDiscoveryUrl)
		{
			if (string.IsNullOrEmpty(serviceDiscoveryUrl)) throw new ArgumentException("Value cannot be null or empty.", nameof(serviceDiscoveryUrl));

			ServiceDiscoveryUrl = serviceDiscoveryUrl;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IServiceDiscoveryService>(context => RestService.For<IServiceDiscoveryService>(ServiceDiscoveryUrl, new RefitSettings(){ HttpMessageHandlerFactory = HttpClientHandlerFactory }))
				.As<IServiceDiscoveryService>()
				.SingleInstance();
		}
	}
}
