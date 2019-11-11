using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerMovementStateChangedMessage : EntityActorMessage
	{
		public Vector2 Direction { get; }

		public bool isMoving => Direction != Vector2.zero;

		public PlayerMovementStateChangedMessage(Vector2 direction)
		{
			Direction = direction;
		}
	}
}
