using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	public sealed class DefaultServerPayloadHandler : IPeerPayloadSpecificMessageHandler<GamePacketPayload, GamePacketPayload>
	{
		private ILog Logger { get; }

		public DefaultServerPayloadHandler(ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, GamePacketPayload payload)
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"Recieved unhandled Packet: {payload.GetType().Name}");
		}
	}
}