using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Movement data that can be used to denote
	/// just a change in world position.
	/// </summary>
	[ProtoContract]
	public sealed class PositionChangeMovementData : IMovementData
	{
		//TODO: Add support for instant vs lerped
		/// <inheritdoc />
		[ProtoMember(1)]
		public long TimeStamp { get; }

		/// <inheritdoc />
		[ProtoMember(2)]
		public Vector3 InitialPosition { get; }

		[ProtoMember(3)]
		public Vector2 Direction { get; }

		[ProtoMember(4)]
		public float Rotation { get; }

		[ProtoIgnore]
		public bool isUserCreated => true;

		/// <inheritdoc />
		public PositionChangeMovementData(long timeStamp, Vector3 initialPosition, Vector2 direction, float rotation)
		{
			TimeStamp = timeStamp;
			InitialPosition = initialPosition;
			Direction = direction;
			Rotation = rotation;
		}

		protected PositionChangeMovementData()
		{
			
		}
	}
}
