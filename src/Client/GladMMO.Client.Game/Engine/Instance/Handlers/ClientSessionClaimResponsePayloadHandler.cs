using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientSessionClaimResponsePayloadHandler : BaseGameClientGameMessageHandler<ClientSessionClaimResponsePayload>
	{
		public ClientSessionClaimResponsePayloadHandler(ILog logger) 
			: base(logger)
		{
		}

		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, ClientSessionClaimResponsePayload payload)
		{
			//TODO: Do any pre-spawn initialization here.
			//Unlike World of Warcraft, GladMMO will send an explict payload that indicates that the local player is spawned.

			//TODO: We need to broadcast this failure, so something can handle it.
			if(!payload.isSuccessful)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to claim session. Reason: {payload.ResultCode}");

				return Task.CompletedTask;
			}

			return Task.CompletedTask;
		}
	}
}
