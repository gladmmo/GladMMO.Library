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
	[GamePayload(GamePayloadOperationCode.PlayerRotationUpdate)]
	public sealed class ClientRotationDataUpdateRequest : GameClientPacketPayload
	{
		/// <summary>
		/// The movement data to update the server with.
		/// </summary>
		[ProtoMember(1)]
		public float Rotation { get; private set; }

		/// <summary>
		/// The time at which the client set this rotation.
		/// </summary>
		[ProtoMember(2)]
		public long TimeStamp { get; private set; }

		//We have to send the position otherwise remote client and server won't know
		//at what point the client turned, and will not simulate close to
		//what the client's view is.
		[ProtoMember(3)]
		public Vector3 ClientCurrentPosition { get; private set; }

		public ClientRotationDataUpdateRequest(float rotation, long timeStamp, Vector3 clientCurrentPosition)
		{
			if (timeStamp <= 0) throw new ArgumentOutOfRangeException(nameof(timeStamp));

			Rotation = rotation;
			TimeStamp = timeStamp;
			ClientCurrentPosition = clientCurrentPosition;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientRotationDataUpdateRequest()
		{

		}
	}
}
