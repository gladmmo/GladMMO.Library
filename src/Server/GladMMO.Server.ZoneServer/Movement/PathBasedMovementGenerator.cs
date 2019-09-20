using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class PathBasedMovementGenerator : BaseMovementGenerator<PathBasedMovementData>
	{
		private int CurrentPathIndex = 0;

		public PathBasedMovementGenerator(PathBasedMovementData movementData) 
			: base(movementData)
		{
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			return entity.transform.transform.position = MovementData.InitialPosition;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			if (CurrentPathIndex > MovementData.MovementPath.Count - 1)
				return entity.transform.transform.position;

			int yOffet = Math.Sign((int)MovementData.MovementPath[CurrentPathIndex].z - (int)CurrentPosition.z);
			int xOffet = Math.Sign((int)MovementData.MovementPath[CurrentPathIndex].x - (int)CurrentPosition.x);

			if(yOffet == 0 && 0 == xOffet)
			{
				CurrentPathIndex++;
				return InternalUpdate(entity, currentTime);
			}
			else
				return entity.transform.position = new Vector3(entity.transform.position.x + xOffet, entity.transform.position.y, entity.transform.position.z + yOffet);
		}
	}
}
