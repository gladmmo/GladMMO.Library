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
	public sealed class SMSG_PARTY_COMMAND_RESULT_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_PARTY_COMMAND_RESULT_Payload>
	{
		/// <inheritdoc />
		public SMSG_PARTY_COMMAND_RESULT_PayloadHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_PARTY_COMMAND_RESULT_Payload payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved {nameof(SMSG_PARTY_COMMAND_RESULT_Payload)} for Operation: {payload.Operation} Player: {payload.PlayerName} Result: {payload.Result}.");

			return Task.CompletedTask;
		}
	}
}