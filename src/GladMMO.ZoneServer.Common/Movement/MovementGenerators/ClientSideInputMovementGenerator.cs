using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: Refactor with server-side input generator
	public sealed class ClientSideInputMovementGenerator : BaseMovementGenerator<PositionChangeMovementData>
	{
		private Vector3 CachedMovementDirection;

		//TODO: We shouldn't do this here
		private float DefaultPlayerSpeed = 3.0f;

		private long LastMovementUpdateTime { get; set; }

		private CharacterController Controller { get; }

		public ClientSideInputMovementGenerator(PositionChangeMovementData movementData, [NotNull] CharacterController controller) 
			: base(movementData)
		{
			//Don't null check, could be on the wrong thread.
			Controller = controller ?? throw new ArgumentNullException(nameof(controller));
		}

		protected override void Start([NotNull] GameObject entity, long currentTime)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (Controller == null) throw new ArgumentNullException(nameof(Controller));

			//Sets the new authoratively specified movement position.
			entity.transform.position = MovementData.InitialPosition;

			//Now, we should also create the movement direction
			CachedMovementDirection = new Vector3(MovementData.Direction.x, 0.0f, MovementData.Direction.y).normalized;
			LastMovementUpdateTime = MovementData.TimeStamp;
		}

		/// <inheritdoc />
		protected override void InternalUpdate(GameObject entity, long currentTime)
		{
			//TODO: We should have real handling at some point.
			float diff = DiffFromStartTime(currentTime);

			//gravity
			//Don't need to subtract the cached direction Y because it should be 0, or treated as 0.
			CachedMovementDirection.y = (-9.8f * diff);
			Controller.Move(CachedMovementDirection * diff);

			//Our new last movement time is now the current time.
			LastMovementUpdateTime = currentTime;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float DiffFromStartTime(long currentTime)
		{
			return (float)(currentTime - LastMovementUpdateTime) / TimeSpan.TicksPerSecond;
		}
	}
}
