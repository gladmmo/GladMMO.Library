using System;
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
		public Vector2 MovementInput { get; }

		/// <inheritdoc />
		public ClientMovementDataUpdateRequest(Vector2 movementInput)
		{
			MovementInput = movementInput;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientMovementDataUpdateRequest()
		{
			
		}
	}
}
