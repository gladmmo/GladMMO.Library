using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	/// <summary>
	/// This is the default handler that is invoked when an unknown payload is encountered.
	/// Or a payload is encountered that doesn't have a registered handler.
	/// </summary>
	public sealed class ZoneServerDefaultRequestHandler : IPeerPayloadSpecificMessageHandler<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>>
	{
		private ILog Logger { get; }

		/// <inheritdoc />
		public ZoneServerDefaultRequestHandler(ILog logger)
		{
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			Logger = logger;
		}

		/// <inheritdoc />
		public Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, GameClientPacketPayload payload)
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"Recieved unhandable Payload: {payload.GetType().Name} ConnectionId: {context.Details.ConnectionId}");

			return Task.CompletedTask;
		}
	}
}
