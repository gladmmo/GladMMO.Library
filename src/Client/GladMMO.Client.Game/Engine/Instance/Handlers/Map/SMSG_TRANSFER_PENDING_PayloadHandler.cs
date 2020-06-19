using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_TRANSFER_PENDING_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_TRANSFER_PENDING_Payload>
	{
		private IMapTransferService MapTransferService { get; }

		public SMSG_TRANSFER_PENDING_PayloadHandler(ILog logger,
			[NotNull] IMapTransferService mapTransferService)
			: base(logger)
		{
			MapTransferService = mapTransferService ?? throw new ArgumentNullException(nameof(mapTransferService));
		}

		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_TRANSFER_PENDING_Payload payload)
		{
			//Transfer to the map.
			await MapTransferService.TransferToMapAsync(payload.MapId);

			//Now that we've loaded successfully, we need to tell the server we've completed
			//with the teleport ACK
			//TODO: This even get sent after disabling??
			await context.PayloadSendService.SendMessage(new MSG_MOVE_WORLDPORT_ACK_Payload());
		}
	}
}
 