using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Logging;
using GladNet;
using ProtoBuf;

namespace GladMMO
{
	public sealed class GameServerNetworkClientAutofacModule : Module
	{
		//TODO: We need to clean this up on returning to the titlescreen or something. Assuming we aren't auto-disconnected.
		private static IManagedNetworkClient<GamePacketPayload, GamePacketPayload> GloballyManagedClient { get; set; }

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(context => LogLevel.All)
				.As<LogLevel>()
				.SingleInstance();

			builder.Register<IManagedNetworkClient<GamePacketPayload, GamePacketPayload>>(context =>
				{
					//The idea here is if the global network client it's null we should use it as the instance.
					if(GloballyManagedClient == null || !GloballyManagedClient.isConnected)
						return GloballyManagedClient = new WoWClientWriteServerReadProxyPacketPayloadReaderWriterDecorator<DotNetTcpClientNetworkClient, GamePacketPayload, GamePacketPayload, IGamePacketPayload>(new DotNetTcpClientNetworkClient(), context.Resolve<INetworkSerializationService>())
							.AsManaged();
					else
						return GloballyManagedClient;
				})
				.As<IManagedNetworkClient<GamePacketPayload, GamePacketPayload>>()
				.As<IPeerPayloadSendService<GamePacketPayload>>()
				.As<IPayloadInterceptable>()
				.As<IConnectionService>()
				.SingleInstance();

			builder.RegisterType<DefaultMessageContextFactory>()
				.As<IPeerMessageContextFactory>()
				.SingleInstance();

			builder.RegisterType<PayloadInterceptMessageSendService<GamePacketPayload>>()
				.As<IPeerRequestSendService<GamePacketPayload>>()
				.SingleInstance();

			//Now, with the new design we also have to register the game client itself
			builder.RegisterType<GameNetworkClient>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
