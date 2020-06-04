using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;

namespace GladMMO
{
	
	public sealed class ClientCharacterControllerInputMovementGenerator : CharacterControllerInputMovementGenerator
	{
		//This property mostly exists to allow for callers who may know rotation shouldn't be set can deny it.
		private bool ShouldSetRotation { get; }

		public ClientCharacterControllerInputMovementGenerator(MovementInfo movementData, Lazy<CharacterController> controller, bool shouldSetRotation = true) 
			: base(movementData, controller)
		{
			ShouldSetRotation = shouldSetRotation;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			base.Start(entity, currentTime);

			//Reason: See https://forum.unity.com/threads/does-transform-position-work-on-a-charactercontroller.36149/
			Controller.Value.enabled = false;
			//Sets the new authoratively specified movement position.
			entity.transform.position = MovementData.Position.ToUnityVector();
			if (ShouldSetRotation)
				entity.transform.eulerAngles = new Vector3(entity.transform.eulerAngles.x, MovementData.Orientation, entity.transform.eulerAngles.z);
			Controller.Value.enabled = true;

			//We use the server set position here.
			return entity.transform.position;
		}
	}
}
