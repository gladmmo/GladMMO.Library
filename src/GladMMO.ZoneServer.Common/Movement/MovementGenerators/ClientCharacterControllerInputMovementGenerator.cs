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
		private float Rotation { get; } //mutable now so we can turn it off.

		private bool IsLocalClient { get; }

		public ClientCharacterControllerInputMovementGenerator(MovementInfo movementData, Lazy<CharacterController> controller, EntityMovementSpeed movementSpeedCollection, bool isLocalClient = false) 
			: base(movementData, controller, movementSpeedCollection)
		{
			IsLocalClient = isLocalClient;

			//TODO: Centralize this rotation stuff.
			if(!IsLocalClient)
				Rotation = -((MovementData.Orientation) / (2.0f * (float) Math.PI) * 360.0f) % 360.0f;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			if (IsLocalClient)
			{
				return base.Start(entity, currentTime);
			}
			else
				base.Start(entity, currentTime);

			//Reason: See https://forum.unity.com/threads/does-transform-position-work-on-a-charactercontroller.36149/
			Controller.Value.enabled = false;

			//Sets the new authoratively specified movement position.
			//Directly set to the current position incase we're not there.
			entity.transform.position = MovementData.Position.ToUnityVector();

			if (!IsLocalClient)
				entity.transform.eulerAngles = new Vector3(entity.transform.eulerAngles.x, Rotation, entity.transform.eulerAngles.z);

			Controller.Value.enabled = true;

			//We use the server set position here.
			return entity.transform.position;
		}

		protected override float DiffFromStartTime(long currentTime)
		{
			if (!IsLocalClient)
				return base.DiffFromStartTime(currentTime);
			else
				return Time.deltaTime; //local client should use SUPER accurate Time.deltaTime. Too noticable locally due to camera otherwise.
		}

		protected override void PredictValidationCheck(long currentTime)
		{
			//Don't validate local client predict/continued movement generation.
			if (!IsLocalClient)
				base.PredictValidationCheck(currentTime);
		}
	}
}
