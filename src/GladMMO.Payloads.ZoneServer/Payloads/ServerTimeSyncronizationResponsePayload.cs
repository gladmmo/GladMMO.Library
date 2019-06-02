using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Response sent by the server as a response to <see cref="ServerTimeSyncronizationRequestPayload"/>.
	/// Syncronizes the time between the server and client.
	/// Used for clock/time based event syncronization.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.ServerTimeSyncronization)]
	public sealed class ServerTimeSyncronizationResponsePayload : GameServerPacketPayload
	{
		/// <summary>
		/// The current local time (in ticks).
		/// </summary>
		[ProtoMember(1)]
		public long SentLocalTime { get; private set; }

		/// <summary>
		/// The time stamp from the server.
		/// </summary>
		[ProtoMember(2)]
		public long ServerTime { get; private set; }

		/// <inheritdoc />
		public ServerTimeSyncronizationResponsePayload(long sentLocalTime, long serverTime)
		{
			if(sentLocalTime <= 0) throw new ArgumentOutOfRangeException(nameof(sentLocalTime));
			if(serverTime <= 0) throw new ArgumentOutOfRangeException(nameof(serverTime));

			SentLocalTime = sentLocalTime;
			ServerTime = serverTime;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ServerTimeSyncronizationResponsePayload()
		{

		}
	}
}
