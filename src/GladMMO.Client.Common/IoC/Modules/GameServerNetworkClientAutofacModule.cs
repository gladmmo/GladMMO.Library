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
		private static IManagedNetworkClient<GamePacketPayload, GamePacketPayload> GloballyManagedClient { get; set; }

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(context => LogLevel.All)
				.As<LogLevel>()
				.SingleInstance();

			builder.Register<IManagedNetworkClient<GamePacketPayload, GamePacketPayload>>(context =>
				{
					//TODO: We used to not do this, and persist the client. But now we just DISCONNECT
					GloballyManagedClient?.DisconnectAsync(0);

					return GloballyManagedClient = new WoWClientWriteServerReadProxyPacketPayloadReaderWriterDecorator<DotNetTcpClientNetworkClient, GamePacketPayload, GamePacketPayload, IGamePacketPayload>(new DotNetTcpClientNetworkClient(), context.Resolve<INetworkSerializationService>())
						.AsManaged();
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
