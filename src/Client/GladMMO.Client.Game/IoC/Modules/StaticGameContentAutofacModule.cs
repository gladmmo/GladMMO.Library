using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class StaticGameContentAutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DefaultContentIconDataCollection>()
				.As<IContentIconDataCollection>()
				.SingleInstance();
		}
	}
}
