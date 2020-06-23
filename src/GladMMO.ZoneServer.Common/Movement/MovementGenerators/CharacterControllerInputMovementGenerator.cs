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

		//TODO: Update gravity
		//https://www.mmo-champion.com/threads/1955227-If-you-were-ver-curious-about-how-fast-players-fall-in-wow
		//https://us.forums.blizzard.com/en/wow/t/has-anyone-figured-out-terminal-velocity-in-wow/385478/21
		/*Using a run macro, it has been calculated

		Standard Run Speed: 7 yards / sec
		Also a Mile ever 4 mins 12 sec.
		Falling: 58.75 yards / sec!
		Terminal Velocity: 58.75 yards / sec = 120.1705 mph*/
		public const float CHARACTERCONTROLLER_GRAVITY_SPEED = -(9.8f * 9.8f);

		private long LastMovementUpdateTime { get; set; }

		protected Lazy<CharacterController> Controller { get; }

		private EntityMovementSpeed MovementSpeedCollection { get; }

		public CharacterControllerInputMovementGenerator(MovementInfo movementData, [NotNull] Lazy<CharacterController> controller,
			[NotNull] EntityMovementSpeed movementSpeedCollection) 
			: base(movementData)
		{
			Controller = controller ?? throw new ArgumentNullException(nameof(controller));
			MovementSpeedCollection = movementSpeedCollection ?? throw new ArgumentNullException(nameof(movementSpeedCollection));
		}

		public CharacterControllerInputMovementGenerator(MovementInfo movementData, [NotNull] Lazy<CharacterController> controller, Vector3 initialPosition,
			[NotNull] EntityMovementSpeed movementSpeedCollection)
			: base(movementData, initialPosition)
		{
			Controller = controller ?? throw new ArgumentNullException(nameof(controller));
			MovementSpeedCollection = movementSpeedCollection ?? throw new ArgumentNullException(nameof(movementSpeedCollection));
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

			//TODO: Remote WoW client causes this calculation:
			/*Latency: 125 RemoteTime: 2216242605 Offset: -1733671480
			DIFF > 5 SECONDS: 2111631 MoveInfo TimeStamp: 104696689*/
			//Therefore, we must revisit this and for now use local client timestamp at info start using.
			LastMovementUpdateTime = currentTime; //TODO: Client uses milliseconds since startup.

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
			else if (Math.Abs(diff) < float.Epsilon)
				return entity.transform.position;

			//Some debug code
			if(diff > 5.0f)
				Debug.LogError($"DIFF > 5 SECONDS: {diff} MoveInfo TimeStamp: {MovementData.TimeStamp}");

			//gravity
			//Don't need to subtract the cached direction Y because it should be 0, or treated as 0.
			//Gravity is not applied if the unit is GROUNDED or the unit is flying.
			if(!Controller.Value.isGrounded && ((MovementData.MoveFlags & MovementFlag.MOVEMENTFLAG_FLYING) == 0)) //this is to prevent stutter, mostly matters for local
				CachedMovementDirection.y = (CHARACTERCONTROLLER_GRAVITY_SPEED * diff);

			//Debug.Log($"Move: {CachedMovementDirection} Diff: {diff}");
			Controller.Value.Move(entity.transform.worldToLocalMatrix.inverse * CachedMovementDirection * diff * CalculateMovementSpeed());

			//Our new last movement time is now the current time.
			LastMovementUpdateTime = currentTime;

			return entity.transform.position;
		}

		//I know virtuals likely cannot be inlined, but leaving this attribute from the past.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual float DiffFromStartTime(long currentTime)
		{
			float diff = (float)(currentTime - LastMovementUpdateTime) / 1000.0f; //it's in milliseconds now.

			//Special case of rounding error can cause small negative diff from local
			//Remote clients timestamps aren't adjusted by server so we get what they sent
			//which if there is a drift or lack of syncronization it may end up negative.
			if (diff < 0.0f)
				return 0.0f; //we do this so it moves a tiny bit at least. This is kind of a hack to prevent Moving movement generators from stationary time desync

			return diff;
		}

		//Characters default movement speed is extremely fast by real-world standards. The standard running speed is 7 yards per second
		//See: https://wowwiki.fandom.com/wiki/Speed
		private float CalculateMovementSpeed()
		{
			return MovementSpeedCollection[UnitMoveType.MOVE_RUN];
		}
	}
}
