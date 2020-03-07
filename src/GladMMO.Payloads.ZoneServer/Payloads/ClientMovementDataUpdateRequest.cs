using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using ProtoBuf;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Payload sent by the client to update the server about local client
	/// movement data.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.MovementDataUpdate)]
	public sealed class ClientMovementDataUpdateRequest : GameClientPacketPayload
	{
		/// <summary>
		/// The movement data to update the server with.
		/// </summary>
		[ProtoMember(1)]
		public Vector2 MovementInput { get; private set; }

		/// <summary>
		/// The timestamp of the movement input.
		/// </summary>
		[ProtoMember(2)]
		public long Timestamp { get; private set; }

		/// <summary>
		/// The current client's position as it views itself
		/// in the movement simulation.
		/// </summary>
		[ProtoMember(3)]
		public Vector3 CurrentClientPosition { get; private set; }

		public ClientMovementDataUpdateRequest(Vector2 movementInput, long timestamp, Vector3 currentClientPosition)
		{
			if (timestamp <= 0) throw new ArgumentOutOfRangeException(nameof(timestamp));

			MovementInput = movementInput;
			Timestamp = timestamp;
			CurrentClientPosition = currentClientPosition;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientMovementDataUpdateRequest()
		{
			
		}
	}
}
