using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_TIME_SYNC_REQ_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_TIME_SYNC_REQ_Payload>
	{
		private INetworkTimeService TimeService { get; }

		/// <inheritdoc />
		public SMSG_TIME_SYNC_REQ_PayloadHandler(ILog logger, INetworkTimeService timeService)
			: base(logger)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		//TODO: This is a work in progress, we need a time service.
		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_TIME_SYNC_REQ_Payload payload)
		{
			context.PayloadSendService.SendMessage(new CMSG_TIME_SYNC_RESP_Payload(payload.SynchronizationCounter, (uint)TimeService.MillisecondsSinceStartup));
			return Task.CompletedTask;
		}
	}
}