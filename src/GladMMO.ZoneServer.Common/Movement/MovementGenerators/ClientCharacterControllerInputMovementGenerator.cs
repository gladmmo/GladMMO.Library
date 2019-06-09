using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;

namespace GladMMO
{
	
	public sealed class ClientCharacterControllerInputMovementGenerator : CharacterControllerInputMovementGenerator
	{
		public ClientCharacterControllerInputMovementGenerator(PositionChangeMovementData movementData, Lazy<CharacterController> controller) 
			: base(movementData, controller)
		{

		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			base.Start(entity, currentTime);

			//Reason: See https://forum.unity.com/threads/does-transform-position-work-on-a-charactercontroller.36149/
			Controller.Value.enabled = false;
			//Sets the new authoratively specified movement position.
			entity.transform.position = MovementData.InitialPosition;
			Controller.Value.enabled = true;

			//We use the server set position here.
			return MovementData.InitialPosition;
		}
	}
}
