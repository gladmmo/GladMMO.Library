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
	[GamePayload(GamePayloadOperationCode.PlayerRotationUpdate)]
	public sealed class ClientRotationDataUpdateRequest : GameClientPacketPayload
	{
		/// <summary>
		/// The movement data to update the server with.
		/// </summary>
		[ProtoMember(1)]
		public float Rotation { get; private set; }

		/// <inheritdoc />
		public ClientRotationDataUpdateRequest(float rotation)
		{
			Rotation = rotation;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientRotationDataUpdateRequest()
		{
			
		}
	}
}
