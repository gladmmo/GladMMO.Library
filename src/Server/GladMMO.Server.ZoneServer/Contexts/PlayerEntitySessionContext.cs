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

		public IConnectionService Connection { get; }

		/// <inheritdoc />
		public PlayerEntitySessionContext([NotNull] IPeerPayloadSendService<GameServerPacketPayload> zoneSession, int connectionId, [NotNull] IConnectionService connection)
		{
			//TODO: Maybe this should be a uint?
			if(connectionId <= 0) throw new ArgumentOutOfRangeException(nameof(connectionId));

			ZoneSession = zoneSession ?? throw new ArgumentNullException(nameof(zoneSession));
			ConnectionId = connectionId;
			Connection = connection ?? throw new ArgumentNullException(nameof(connection));
		}
	}
}
