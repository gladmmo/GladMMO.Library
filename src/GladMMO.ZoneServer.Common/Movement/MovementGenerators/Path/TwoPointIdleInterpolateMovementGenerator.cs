using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	public sealed class TwoPointIdleInterpolateMovementGenerator : BaseMovementGenerator
	{
		public Vector3 TargetPosition { get; }

		public Vector3 StartPosition { get; set; }

		public EntityMovementSpeed MovementSpeeds { get; }

		public long StartTimeStamp { get; set; }

		public const int MAX_TIME = 2 * 1000;

		//Used for path generation, so client usually should not be able to break it.
		public override bool IsClientInterruptable => false;

		public TwoPointIdleInterpolateMovementGenerator(Vector3 targetPosition, EntityMovementSpeed movementSpeeds)
		{
			TargetPosition = targetPosition;
			MovementSpeeds = movementSpeeds;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			StartTimeStamp = currentTime;
			return StartPosition = entity.transform.position;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			if (StartTimeStamp + MAX_TIME < currentTime)
			{
				StopGenerator();
				return entity.transform.position = TargetPosition;
			}

			return entity.transform.position = Vector3.Lerp(StartPosition, TargetPosition, (currentTime - StartTimeStamp) / (float)MAX_TIME);
		}
	}
}
