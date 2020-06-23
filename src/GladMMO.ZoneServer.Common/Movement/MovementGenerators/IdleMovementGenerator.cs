using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: Optimize idle to prevent Vector copying.
	/// <summary>
	/// The do nothing no good movement generator.
	/// </summary>
	public sealed class IdleMovementGenerator : MoveGenerator
	{
		private Vector3 InitialPosition { get; }

		private float? YAxisRotation { get; } = null;

		public IdleMovementGenerator(Vector3 initialPosition, float yAxisRotation)
		{
			InitialPosition = initialPosition;
			YAxisRotation = yAxisRotation;
		}

		public IdleMovementGenerator(Vector3 initialPosition)
		{
			InitialPosition = initialPosition;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			//Immediately we're idle so let's stop the generator.
			StopGenerator();

			if (YAxisRotation.HasValue)
				entity.transform.SetPositionAndRotation(InitialPosition, Quaternion.AngleAxis(YAxisRotation.Value, Vector3.up));
			else
				return entity.transform.position = InitialPosition;

			return entity.transform.position;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			//Let's do nothing, forever!
			return InitialPosition;
		}
	}
}
