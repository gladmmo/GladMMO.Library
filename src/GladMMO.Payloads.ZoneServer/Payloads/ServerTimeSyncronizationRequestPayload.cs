using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Request sent by the client to syncronize
	/// the time between the client and server.
	/// Used for clock/time based event syncronization.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.ServerTimeSyncronization)]
	public sealed class ServerTimeSyncronizationRequestPayload : GameClientPacketPayload
	{
		/// <summary>
		/// The current local time (in ticks).
		/// </summary>
		[ProtoMember(1)]
		public long CurrentLocalTime { get; private set; }

		/// <inheritdoc />
		public ServerTimeSyncronizationRequestPayload(long currentLocalTime)
		{
			if(currentLocalTime <= 0) throw new ArgumentOutOfRangeException(nameof(currentLocalTime));

			CurrentLocalTime = currentLocalTime;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ServerTimeSyncronizationRequestPayload()
		{
			
		}
	}
}
