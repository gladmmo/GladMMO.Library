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

		private float YAxisRotation { get; }

		public IdleMovementGenerator(Vector3 initialPosition, float yAxisRotation)
		{
			InitialPosition = initialPosition;
			YAxisRotation = yAxisRotation;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			//Immediately we're idle so let's stop the generator.
			StopGenerator();

			entity.transform.SetPositionAndRotation(InitialPosition, Quaternion.AngleAxis(YAxisRotation, Vector3.up));

			return entity.transform.position;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			//Let's do nothing, forever!
			return InitialPosition;
		}
	}
}
