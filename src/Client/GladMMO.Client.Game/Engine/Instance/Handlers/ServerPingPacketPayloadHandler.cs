using System;
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
	public sealed class ServerPingPacketPayloadHandler : BaseGameClientGameMessageHandler<ServerPingPacketPayload>
	{
		public ServerPingPacketPayloadHandler(ILog logger)
			: base(logger)
		{

		}

		//TODO: This is a work in progress, we need a time service.
		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, ServerPingPacketPayload payload)
		{
			//We send time sync first since we need to have a good grasp of the current network time before
			//we even spawn into the world and start recieveing the world states.
			context.PayloadSendService.SendMessageImmediately(new ServerTimeSyncronizationRequestPayload(DateTime.UtcNow.Ticks))
				.ConfigureAwait(false);

			return Task.CompletedTask;
		}
	}
}