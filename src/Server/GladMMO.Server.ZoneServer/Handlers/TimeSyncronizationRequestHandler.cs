using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class TimeSyncronizationRequestHandler : BaseServerRequestHandler<ServerTimeSyncronizationRequestPayload>
	{
		/// <inheritdoc />
		public TimeSyncronizationRequestHandler([NotNull] ILog logger) 
			: base(logger)
		{
			
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ServerTimeSyncronizationRequestPayload payload)
		{
			//TODO: Is ticks best? Or Unity3D deltatime since startup? Or Enviroment ticks?
			//TODO: Do we need to store the time diff? To track latency serverside for some reason?
			await context.PayloadSendService.SendMessage(new ServerTimeSyncronizationResponsePayload(payload.CurrentLocalTime, DateTime.UtcNow.Ticks))
				.ConfigureAwaitFalse();
		}
	}
}
