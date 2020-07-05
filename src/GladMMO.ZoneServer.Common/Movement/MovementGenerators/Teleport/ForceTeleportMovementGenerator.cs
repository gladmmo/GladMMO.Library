using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	public sealed class ForceTeleportMovementGenerator : BaseMovementGenerator<MovementInfo>
	{
		//Force teleports MUST resolve, we cannot be interrupt for any reason EVER.
		public override bool IsClientInterruptable => false;

		public ForceTeleportMovementGenerator(MovementInfo movementData) 
			: base(movementData)
		{
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			//Just force teleport them to location.
			Vector3 teleportPosition = MovementData.Position.ToUnityVector();

			//TODO: Handle orientation/rotation
			//TODO: This could mess with Physics. How do we resolve this??
			entity.transform.position = teleportPosition;

			StopGenerator();

			return teleportPosition;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			return entity.transform.position;
		}
	}
}
