using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LoggableUnknownPacketHandler : BaseGameClientGameMessageHandler<LoggableUnknownOpcodePayload>
	{
		public LoggableUnknownPacketHandler(ILog logger) 
			: base(logger)
		{

		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, LoggableUnknownOpcodePayload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Unhandled Packet: {payload.OpCode}");

			return Task.CompletedTask;
		}
	}
}
