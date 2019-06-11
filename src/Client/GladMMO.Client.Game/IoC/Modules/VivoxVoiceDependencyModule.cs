using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class VivoxVoiceDependencyModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<VivoxUnity.Client>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<PositionalVoiceEntityTickable>()
				.AsSelf();

			builder.RegisterInstance(new DefaultPositionVoiceChannelsCollection())
				.AsImplementedInterfaces()
				.AsSelf()
				.SingleInstance();
		}
	}
}
