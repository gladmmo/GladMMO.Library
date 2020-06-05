using System; using FreecraftCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Common.Logging;
using FreecraftCore.Serializer;
using GladNet;
using Module = Autofac.Module;

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
			builder.RegisterType<SerializerService>()
				.AsSelf()
				.As<ISerializerService>()
				.OnActivated(args =>
				{
					//TODO: Automate discovery of payload types.
					args.Instance.RegisterType<GamePacketPayload>();
					args.Instance.RegisterType<ServerPacketHeader>();
					args.Instance.RegisterType<OutgoingClientPacketHeader>();

					ILog logger = args.Context.Resolve<ILog>();

					try
					{
						Type[] types = typeof(MSG_MOVE_START_STRAFE_LEFT_Payload).Assembly
							.GetExportedTypes()
							.Where(t => t.IsAssignableTo<GamePacketPayload>())
							.ToArray();

						foreach(Type t in types)
						{
							if(logger.IsInfoEnabled)
								logger.Info($"Registered type: {t}");

							//TODO: This packet is broken for serialization, need to investigate.
							if (typeof(SMSG_ADDON_INFO_Payload) == t)
								continue;

							args.Instance.RegisterType(t);
						}
					}
					catch (Exception e)
					{
						if(logger.IsErrorEnabled)
							logger.Error($"Failed to Register Packet Types. Reason: {e.Message}");

						throw;
					}

					args.Instance.Compile();
				})
				.SingleInstance();

			builder.RegisterType<FreecraftCoreGladNetSerializerAdapter>()
				.AsSelf()
				.As<INetworkSerializationService>()
				.SingleInstance();
		}
	}
}