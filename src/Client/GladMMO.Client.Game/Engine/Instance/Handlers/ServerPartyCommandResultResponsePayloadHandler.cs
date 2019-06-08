using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	/*[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class ServerPartyCommandResultResponsePayloadHandler : BaseGameClientGameMessageHandler<ServerPartyCommandResultResponse>
	{
		/// <inheritdoc />
		public ServerPartyCommandResultResponsePayloadHandler(ILog logger) 
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, ServerPartyCommandResultResponse payload)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved {nameof(ServerPartyCommandResultResponse)} for Operation: {payload.Operation} Player: {payload.PlayerName} Result: {payload.Result}.");

			return Task.CompletedTask;
		}
	}*/
}
