using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message for broadcasting a server packet to an entire entity actor's interest set.
	/// </summary>
	public sealed class BroadcastToInterestSetMessage : EntityActorMessage
	{
		/// <summary>
		/// Server message to broadcast.
		/// </summary>
		public GameServerPacketPayload Message { get; }

		/// <summary>
		/// Indicates if the <see cref="Message"/> should be sent to the local actor's client.
		/// </summary>
		public bool SendToSelf { get; }

		public BroadcastToInterestSetMessage([NotNull] GameServerPacketPayload message, bool sendToSelf)
		{
			Message = message ?? throw new ArgumentNullException(nameof(message));
			SendToSelf = sendToSelf;
		}
	}
}
