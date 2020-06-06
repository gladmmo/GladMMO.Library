using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	/// <summary>
	/// Contract for service that allows the client to send ITSELF a server message/packet.
	/// </summary>
	public interface ILocalClientMessageSenderService
	{
		Task<bool> HandleMessage(GamePacketPayload payload);
	}

	public sealed class DefaultLocalClientMessageSenderService : ILocalClientMessageSenderService
	{
		private MessageHandlerService<GamePacketPayload, GamePacketPayload> Handlers { get; }

		private IPeerMessageContextFactory ContextFactory { get; }

		private IManagedNetworkClient<GamePacketPayload, GamePacketPayload> Client { get; }

		private PayloadInterceptMessageSendService<GamePacketPayload> Intercept { get; }

		public DefaultLocalClientMessageSenderService([NotNull] MessageHandlerService<GamePacketPayload, GamePacketPayload> handlers,
			[NotNull] IPeerMessageContextFactory contextFactory,
			[NotNull] IManagedNetworkClient<GamePacketPayload, GamePacketPayload> client)
		{
			Handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
			ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
			Client = client ?? throw new ArgumentNullException(nameof(client));

			Intercept = new PayloadInterceptMessageSendService<GamePacketPayload>(client, client);
		}

		public bool CanHandle(NetworkIncomingMessage<GamePacketPayload> message)
		{
			return Handlers.CanHandle(message);
		}

		public Task<bool> TryHandleMessage(IPeerMessageContext<GamePacketPayload> context, NetworkIncomingMessage<GamePacketPayload> message)
		{
			return Handlers.TryHandleMessage(context, message);
		}

		public Task<bool> HandleMessage(GamePacketPayload payload)
		{
			return TryHandleMessage(ContextFactory.Create(Client, Client, Intercept), new NetworkIncomingMessage<GamePacketPayload>(new HeaderlessPacketHeader(0), payload));
		}
	}
}
