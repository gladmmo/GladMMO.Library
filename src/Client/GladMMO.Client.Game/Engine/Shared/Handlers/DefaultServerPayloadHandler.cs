using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultServerPayloadHandler : IPeerPayloadSpecificMessageHandler<GameServerPacketPayload, GameClientPacketPayload>
	{
		/// <inheritdoc />
		public async Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, GameServerPacketPayload payload)
		{
			Debug.LogWarning($"Recieved unhandled Packet: {payload.GetType().Name}");
		}
	}
}