using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GladNet;
using Refit;
using UnityEngine;

namespace GladMMO
{
	public sealed class AuthenticationDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		public AuthenticationDependencyAutofacModule()
		{
			
		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<IAuthenticationService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				return new AsyncEndpointAuthenticationService(QueryForRemoteServiceEndpoint(serviceDiscovery, "Authentication"), new RefitSettings() { HttpMessageHandlerFactory = () => new FiddlerEnabledWebProxyHandler() });
			});
		}
	}
}
