using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public class LocalClientInterpolatedCorrectionMovementGenerator : BaseMovementGenerator<MovementInfo>
	{
		protected Lazy<CharacterController> Controller { get; }

		public bool ShouldSetRotation { get; }

		//See: AvatarLerper for an example of this.
		private float LerpPower { get; } = 0.4f;

		public LocalClientInterpolatedCorrectionMovementGenerator(MovementInfo movementData, [NotNull] Lazy<CharacterController> controller, bool shouldSetRotation = true) 
			: base(movementData)
		{
			Controller = controller ?? throw new ArgumentNullException(nameof(controller));
			ShouldSetRotation = shouldSetRotation;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			if (ShouldSetRotation)
				//We must still set rotation here
				entity.transform.eulerAngles = new Vector3(entity.transform.eulerAngles.x, MovementData.Orientation, entity.transform.eulerAngles.z);

			//Normally, we should set at least the rotation here but this is ONLY
			//for use with the local player, so we basically should do nothing.
			return entity.transform.position;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			//TODO: We should sleep/stop this movement generator after it's completed.
			//Reason: See https://forum.unity.com/threads/does-transform-position-work-on-a-charactercontroller.36149/
			Controller.Value.enabled = false;
			entity.transform.position = Vector3.Lerp(entity.transform.position, MovementData.Position.ToUnityVector(), LerpPower);
			Controller.Value.enabled = true;

			return entity.transform.position;
		}
	}
}
