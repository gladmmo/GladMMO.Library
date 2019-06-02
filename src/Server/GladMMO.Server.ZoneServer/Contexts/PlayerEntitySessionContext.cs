using System;
using System.Collections.Generic;
using System.Text;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class PlayerEntitySessionContext
	{
		public IPeerPayloadSendService<GameServerPacketPayload> ZoneSession { get; }

		/// <summary>
		/// The connection ID of the session.
		/// </summary>
		public int ConnectionId { get; }

		/// <inheritdoc />
		public PlayerEntitySessionContext([NotNull] IPeerPayloadSendService<GameServerPacketPayload> zoneSession, int connectionId)
		{
			//TODO: Maybe this should be a uint?
			if(connectionId <= 0) throw new ArgumentOutOfRangeException(nameof(connectionId));

			ZoneSession = zoneSession ?? throw new ArgumentNullException(nameof(zoneSession));
			ConnectionId = connectionId;
		}
	}
}
