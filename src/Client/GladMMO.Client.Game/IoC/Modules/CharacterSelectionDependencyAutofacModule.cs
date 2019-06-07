using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Refit;

namespace GladMMO
{
	public sealed class CharacterSelectionDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CharacterServiceDependencyAutofacModule>();
		}
	}
}
