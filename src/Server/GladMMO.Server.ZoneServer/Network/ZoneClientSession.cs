using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	/// <summary>
	/// The zone client session.
	/// </summary>
	public sealed class ZoneClientSession : ManagedClientSession<GameServerPacketPayload, GameClientPacketPayload>
	{
		/// <summary>
		/// The message handlers service.
		/// </summary>
		private MessageHandlerService<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>> MessageHandlers { get; }

		//TODO: One day this design fault will be fixed.
		public static MockedPayloadInterceptorService MockedInterceptorService { get; } = new MockedPayloadInterceptorService();

		private ILog Logger { get; }

		/// <inheritdoc />
		public ZoneClientSession(IManagedNetworkServerClient<GameServerPacketPayload, GameClientPacketPayload> internalManagedNetworkClient, 
			SessionDetails details, 
			MessageHandlerService<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>> messageHandlers, 
			[NotNull] ILog logger) 
			: base(internalManagedNetworkClient, details)
		{
			MessageHandlers = messageHandlers ?? throw new ArgumentNullException(nameof(messageHandlers));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public override Task OnNetworkMessageRecieved(NetworkIncomingMessage<GameClientPacketPayload> message)
		{
			return MessageHandlers.TryHandleMessage(new DefaultSessionMessageContext<GameServerPacketPayload>(this.Connection, this.SendService, MockedInterceptorService, Details), message);
		}
	}
}
