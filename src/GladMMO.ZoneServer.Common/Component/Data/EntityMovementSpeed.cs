using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	/// <summary>
	/// Mutable Movement speed component for Entities.
	/// </summary>
	public sealed class EntityMovementSpeed
	{
		private object SyncObj { get; } = new object();

		private float[] MovementSpeeds { get; set; }

		public float this[UnitMoveType moveType]
		{
			get
			{
				int index = CalculateIndex(moveType);

				lock (SyncObj)
					return MovementSpeeds[index];
			}
		}

		private static int CalculateIndex(UnitMoveType moveType)
		{
			//WoW sends Turn rate different order sooooo that needs offset accounted for.
			if(moveType == UnitMoveType.MOVE_TURN_RATE)
				return 9; //uses last index for some reason.
			else if (moveType > UnitMoveType.MOVE_TURN_RATE)
				return (int) moveType - 1;
			else
				return (int) moveType;
		}

		public EntityMovementSpeed([NotNull] float[] movementSpeeds)
		{
			MovementSpeeds = movementSpeeds ?? throw new ArgumentNullException(nameof(movementSpeeds));
		}

		public EntityMovementSpeed()
			: this(new float[9]) //size of UnitMoveType
		{

		}

		public void SetMovementSpeed(UnitMoveType moveType, float speed)
		{
			if(speed < 0) throw new ArgumentOutOfRangeException(nameof(speed));

			if(moveType == UnitMoveType.MOVE_TURN_RATE)
				throw new InvalidOperationException($"Cannot set: {moveType}");

			lock(SyncObj)
				MovementSpeeds[CalculateIndex(moveType)] = speed;
		}

		public void SetMovementSpeeds(float[] speeds)
		{
			if (speeds.Length != MovementSpeeds.Length)
				throw new InvalidOperationException($"Cannot set MovementSpeed to Length: {speeds.Length}. Must be Length: {MovementSpeeds.Length}");

			lock(SyncObj)
				MovementSpeeds = speeds;
		}
	}
}
