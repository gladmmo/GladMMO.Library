using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;

namespace GladMMO
{
	//TODO: Refactor with server-side input generator
	public class CharacterControllerInputMovementGenerator : BaseMovementGenerator<MovementInfo>
	{
		private Vector3 CachedMovementDirection;

		//See: https://wowwiki.fandom.com/wiki/Speed
		//TODO: We shouldn't do this here, still true!
		private float DefaultPlayerSpeed = 7.0f; //Characters are extremely fast by real-world standards. The standard running speed is 7 yards per second

		public const float CHARACTERCONTROLLER_GRAVITY_SPEED = -(9.8f * 9.8f);

		private long LastMovementUpdateTime { get; set; }

		protected Lazy<CharacterController> Controller { get; }

		public CharacterControllerInputMovementGenerator(MovementInfo movementData, [NotNull] Lazy<CharacterController> controller) 
			: base(movementData)
		{
			Controller = controller ?? throw new ArgumentNullException(nameof(controller));
		}

		public CharacterControllerInputMovementGenerator(MovementInfo movementData, [NotNull] Lazy<CharacterController> controller, Vector3 initialPosition)
			: base(movementData, initialPosition)
		{
			Controller = controller ?? throw new ArgumentNullException(nameof(controller));
		}

		protected override Vector3 Start([NotNull] GameObject entity, long currentTime)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (Controller == null) throw new ArgumentNullException(nameof(Controller));

			//Now, we should also create the movement direction\
			Vector3 directionVector = Vector3.zero;

			if (this.MovementData.MoveFlags.HasAnyFlags(MovementFlag.MOVEMENTFLAG_RIGHT | MovementFlag.MOVEMENTFLAG_STRAFE_RIGHT))
				directionVector += Vector3.right;
			else if (this.MovementData.MoveFlags.HasAnyFlags(MovementFlag.MOVEMENTFLAG_LEFT | MovementFlag.MOVEMENTFLAG_STRAFE_LEFT))
				directionVector += Vector3.left;

			if(this.MovementData.MoveFlags.HasAnyFlags(MovementFlag.MOVEMENTFLAG_FORWARD))
				directionVector += Vector3.forward;
			else if(this.MovementData.MoveFlags.HasAnyFlags(MovementFlag.MOVEMENTFLAG_BACKWARD))
				directionVector += Vector3.back;

			directionVector = directionVector.normalized;

			CachedMovementDirection = directionVector;
			LastMovementUpdateTime = MovementData.TimeStamp;

			//Directly set to the current position incase we're not there.
			entity.transform.position = CurrentPosition;

			//If the direction is zero just stop the generator.
			if (this.MovementData.MoveFlags == 0)
			{
				StopGenerator();
			}

			return entity.transform.position;
		}

		/// <inheritdoc />
		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			//TODO: We should have real handling at some point.
			float diff = DiffFromStartTime(currentTime);

			if(diff < 0.0f)
				throw new InvalidOperationException($"Movement diff time is less than 0. Diff: {diff}");

			//gravity
			//Don't need to subtract the cached direction Y because it should be 0, or treated as 0.
			CachedMovementDirection.y = (CHARACTERCONTROLLER_GRAVITY_SPEED * diff);
			Controller.Value.Move(entity.transform.worldToLocalMatrix.inverse * CachedMovementDirection * diff * DefaultPlayerSpeed);

			//Our new last movement time is now the current time.
			LastMovementUpdateTime = currentTime;

			return entity.transform.position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float DiffFromStartTime(long currentTime)
		{
			float diff = (float)(currentTime - LastMovementUpdateTime) / TimeSpan.TicksPerSecond;

			//Special case of rounding error can cause small negative diff from local
			//Remote clients timestamps aren't adjusted by server so we get what they sent
			//which if there is a drift or lack of syncronization it may end up negative.
			if (diff < 0.0f)
				return 0.01f; //we do this so it moves a tiny bit at least. This is kind of a hack to prevent Moving movement generators from stationary time desync

			return diff;
		}
	}
}
