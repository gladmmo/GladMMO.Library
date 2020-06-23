using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	public class LookAtLinearPathMovementGenerator : LinearPathMovementGenerator
	{
		public WorldTransform ToLookAt { get; }

		public LookAtLinearPathMovementGenerator(LinearPathMoveInfo movementData, Vector3 initialPosition, int totalLengthDuration, int timeElapsed, EntityMovementSpeed movementSpeedCollection, [NotNull] WorldTransform toLookAt)
			: base(movementData, initialPosition, totalLengthDuration, timeElapsed, movementSpeedCollection)
		{
			ToLookAt = toLookAt ?? throw new ArgumentNullException(nameof(toLookAt));
		}

		protected override void SetFinalRotation([NotNull] GameObject entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			Vector3 rotation = entity.transform.rotation.eulerAngles;
			float yAxisRotation = CalculateYAxisRotation(entity);

			entity.transform.rotation = Quaternion.Euler(rotation.x, yAxisRotation, rotation.z);
		}

		private float CalculateYAxisRotation(GameObject entity)
		{
			Vector3 direction = (new Vector3(ToLookAt.PositionX, ToLookAt.PositionY, ToLookAt.PositionZ) - entity.transform.position);
			direction = new Vector3(direction.x, 0.0f, direction.z);

			return Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y;
		}

		protected override void InterpolateRotation([NotNull] GameObject entity, Vector3 direction)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			Vector3 rotation = entity.transform.rotation.eulerAngles;
			float yAxisRotation = CalculateYAxisRotation(entity);

			entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, Quaternion.Euler(rotation.x, yAxisRotation, rotation.z), 75.0f * Time.deltaTime);
		}

		public override void Update(GameObject entity, long currentTime)
		{
			//Even if finished, we should keep ticking rotation
			if (isFinished)
			{
				InterpolateRotation(entity, Vector3.zero);
			}
			else
				base.Update(entity, currentTime);
		}
	}
}