using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	//This basically just logs unhandled opcodes or unimplemented opcodes.
	/*public sealed class LoggableUnknownOpcodePayloadHandler : BaseGameClientGameMessageHandler<LoggableUnknownOpcodePayload>
	{
		/// <inheritdoc />
		public LoggableUnknownOpcodePayloadHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, LoggableUnknownOpcodePayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Encountered Unhandled Opcode: {payload.OpCode} Length: {payload.PacketLength}");

			return Task.CompletedTask;
		}
	}*/
}
