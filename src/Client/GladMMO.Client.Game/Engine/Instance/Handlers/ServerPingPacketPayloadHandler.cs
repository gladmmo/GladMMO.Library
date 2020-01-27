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
			//The issue with time sync every ping is that the remote packet queue could contain a bunch of inputs from rotation/movement
			//which will skew the time sync result. Best to just go with the initial/original time syncronization.
			//context.PayloadSendService.SendMessageImmediately(new ServerTimeSyncronizationRequestPayload(DateTime.UtcNow.Ticks))
			//	.ConfigureAwaitFalse();

			return Task.CompletedTask;
		}
	}
}