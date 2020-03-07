﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class CharacterServiceDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<ICharacterService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				IReadonlyAuthTokenRepository tokenRepository = context.Resolve<IReadonlyAuthTokenRepository>();

				return new AsyncEndpointCharacterService(QueryForRemoteServiceEndpoint(serviceDiscovery, "GameServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(tokenRepository) });
			});
		}
	}
}
