﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IMovementInputChangedEventSubscribable
	{
		event EventHandler<MovementInputChangedEventArgs> OnMovementInputDataChanged;
	}

	public sealed class MovementInputChangedEventArgs : EventArgs, IEquatable<MovementInputChangedEventArgs>
	{
		//The reason we use float is because of controllers
		//They may lightly press forward to WALK.
		//PC players can't really benefit though.
		public float NewVerticalInput { get; }

		public float NewHorizontalInput { get; }

		/// <summary>
		/// Indicates if currently moving.
		/// </summary>
		public bool isMoving => (Math.Abs(NewVerticalInput) > 0.005f) || (Math.Abs(NewHorizontalInput) > 0.005f);

		public bool isHeartBeat { get; }

		/// <inheritdoc />
		public MovementInputChangedEventArgs(float newVerticalInput, float newHorizontalInput, bool isHeartBeat)
		{
			NewVerticalInput = newVerticalInput;
			NewHorizontalInput = newHorizontalInput;
			this.isHeartBeat = isHeartBeat;
		}

		public static bool operator ==(MovementInputChangedEventArgs obj1, MovementInputChangedEventArgs obj2)
		{
			if(ReferenceEquals(obj1, obj2))
				return true;

			if(ReferenceEquals(obj1, null))
				return false;

			if(ReferenceEquals(obj2, null))
				return false;

			return obj1.Equals(obj2);
		}

		public static bool operator !=(MovementInputChangedEventArgs obj1, MovementInputChangedEventArgs obj2)
		{
			return !(obj1 == obj2);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;

			if(obj is MovementInputChangedEventArgs second)
				return (Math.Abs(second.NewHorizontalInput - NewHorizontalInput) < 0.005f) && (Math.Abs(second.NewVerticalInput - NewVerticalInput) < 0.005f);

			return false;
		}

		public bool Equals([NotNull] MovementInputChangedEventArgs other)
		{
			if(other == null)
				return false;

			return NewVerticalInput.Equals(other.NewVerticalInput) && NewHorizontalInput.Equals(other.NewHorizontalInput);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				return (NewVerticalInput.GetHashCode() * 397) ^ NewHorizontalInput.GetHashCode();
			}
		}

		public MovementFlag BuildMovementFlags()
		{
			MovementFlag flags = MovementFlag.MOVEMENTFLAG_NONE;

			if (NewHorizontalInput > 0.0f)
				flags |= MovementFlag.MOVEMENTFLAG_STRAFE_RIGHT;
			else if (NewHorizontalInput < 0.0f)
				flags |= MovementFlag.MOVEMENTFLAG_STRAFE_LEFT;

			if (NewVerticalInput > 0.0f)
				flags |= MovementFlag.MOVEMENTFLAG_FORWARD;
			else if (NewVerticalInput < 0.0f)
				flags |= MovementFlag.MOVEMENTFLAG_BACKWARD;

			return flags;
		}
	}
}