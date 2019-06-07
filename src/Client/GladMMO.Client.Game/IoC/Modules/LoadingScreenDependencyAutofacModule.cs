using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class LoadingScreenDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CharacterServiceDependencyAutofacModule>();
		}
	}
}
