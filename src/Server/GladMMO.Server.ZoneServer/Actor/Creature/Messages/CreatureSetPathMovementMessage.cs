using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class CreatureSetPathMovementMessage : EntityActorMessage
	{
		public Vector3 InitialPosition { get; }

		public Vector3[] PathPoints { get; }

		public CreatureSetPathMovementMessage(Vector3 initialPosition, [NotNull] Vector3[] pathPoints)
		{
			PathPoints = pathPoints ?? throw new ArgumentNullException(nameof(pathPoints));
			InitialPosition = initialPosition;
		}
	}
}
