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
	public sealed class SMSG_QUERY_TIME_RESPONSE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_QUERY_TIME_RESPONSE_Payload>
	{
		private INetworkTimeService TimeService { get; }

		/// <inheritdoc />
		public SMSG_QUERY_TIME_RESPONSE_PayloadHandler(ILog logger, INetworkTimeService timeService)
			: base(logger)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		//TODO: This is a work in progress, we need a time service.
		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_QUERY_TIME_RESPONSE_Payload payload)
		{
			//SMSG_QUERY_TIME_RESPONSE_Payload actually returns a uint32 UNIX timestamp in the form of SECONDS.
			//This is an important distinction to ticks. So we must convert it to ticks.
			TimeService.SetTimeSyncronization(TimeService.LastQueryTime, payload.CurrentTime * TimeSpan.TicksPerSecond); //It's in UNIX timestamp SECONDS.
			return Task.CompletedTask;
		}
	}
}