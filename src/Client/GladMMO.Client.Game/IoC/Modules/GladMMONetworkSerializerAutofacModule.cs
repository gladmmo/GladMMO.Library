using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using GladNet;

namespace GladMMO
{
	public sealed class GladMMONetworkSerializerAutofacModule : Module
	{
		public GladMMONetworkSerializerAutofacModule()
		{

		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{

			//Register the serialization models.
			Unity3DProtobufPayloadRegister unityProtobufRegisteration = new Unity3DProtobufPayloadRegister();
			unityProtobufRegisteration.RegisterDefaults();
			unityProtobufRegisteration.Register();

			builder.RegisterType<ProtobufNetGladNetSerializerAdapter>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}