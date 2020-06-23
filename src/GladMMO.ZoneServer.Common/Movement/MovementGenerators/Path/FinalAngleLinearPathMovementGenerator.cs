using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	public class FinalAngleLinearPathMovementGenerator : LinearPathMovementGenerator
	{
		private float FinalAngle { get; }

		public FinalAngleLinearPathMovementGenerator(LinearPathMoveInfo movementData, Vector3 initialPosition, int totalLengthDuration, int timeElapsed, EntityMovementSpeed movementSpeedCollection, float finalAngle)
			: base(movementData, initialPosition, totalLengthDuration, timeElapsed, movementSpeedCollection)
		{
			FinalAngle = finalAngle;
		}

		protected override void SetFinalRotation([NotNull] GameObject entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			Vector3 rotation = entity.transform.rotation.eulerAngles;
			entity.transform.rotation = Quaternion.Euler(rotation.x, FinalAngle, rotation.z);
		}
	}
}