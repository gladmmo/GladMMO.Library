using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	internal class LinearPointPathState
	{
		public int CurrentIndex { get; }

		public int StartTimeStamp { get; }

		public float Distance { get; }

		public int MillisecondsRequiredUntilNextPoint { get; }

		public LinearPointPathState(Vector3 pointA, Vector3 pointB, float speed, int currentIndex, int startTimeStamp)
		{
			CurrentIndex = currentIndex;
			StartTimeStamp = startTimeStamp;
			Distance = Vector3.Distance(pointA, pointB);
			MillisecondsRequiredUntilNextPoint = (int) (1000 * (Distance / speed));
		}

		public LinearPointPathState(float distance, int millisecondsRequiredUntilNextPoint, int currentIndex, int startTimeStamp)
		{
			Distance = distance;
			MillisecondsRequiredUntilNextPoint = millisecondsRequiredUntilNextPoint;
			CurrentIndex = currentIndex;
			StartTimeStamp = startTimeStamp;
		}
	}

	public class LinearPathMovementGenerator : BaseMovementGenerator<LinearPathMoveInfo>
	{
		protected Vector3[] GeneratedPath { get; }

		protected int TotalLengthDuration { get; }

		protected long StartTimeStamp { get; set; }

		protected long EndTimeStamp { get; set; }

		private LinearPointPathState State { get; set; }

		private EntityMovementSpeed MovementSpeedCollection { get; }

		/// <summary>
		/// The computed modifier generated from the distance traveled over time
		/// compared to the in-time run movement speed.
		/// </summary>
		private float MovementSpeedModifier { get; set; }

		private float PathDistance { get; set; }

		//Used for path generation, so client usually should not be able to break it.
		public override bool IsClientInterruptable => false;

		public LinearPathMovementGenerator(LinearPathMoveInfo movementData, Vector3 initialPosition, int totalLengthDuration, EntityMovementSpeed movementSpeedCollection) 
			: this(movementData, initialPosition, totalLengthDuration, 0, movementSpeedCollection)
		{
			
		}

		public LinearPathMovementGenerator(LinearPathMoveInfo movementData, Vector3 initialPosition, int totalLengthDuration, int timeElapsed, EntityMovementSpeed movementSpeedCollection)
			: base(movementData, initialPosition)
		{
			if (totalLengthDuration < 0) throw new ArgumentOutOfRangeException(nameof(totalLengthDuration));

			TotalLengthDuration = totalLengthDuration;

			//Push the Start timestamp backwards, since some splines we may see start in the middle.
			StartTimeStamp -= timeElapsed;
			MovementSpeedCollection = movementSpeedCollection;

			//Offset
			//G3D::Vector3 middle = (real_path[0] + real_path[last_idx]) / 2.f;

			//All points are OFFSET from middle.
			// offset = middle - real_path[i];
			//We need to do: real_path[i] = middle - offset;
			//or middle - offset = real_path[i];
			Vector3 finalPosition = movementData.FinalPosition.ToUnityVector();
			if(movementData.SplineMiddlePoints.Length > 0)
			{
				Vector3 midpoint = (initialPosition + finalPosition) / 2.0f;
				var midPointsConverted = MovementData
					.SplineMiddlePoints
					.Select(p => midpoint - p.ToUnityVector())
					.ToArray();

				GeneratedPath = new Vector3[1 + 1 + midPointsConverted.Length];

				//I know mid + 1 looks strange, but we do -1 in loop. All good.
				GeneratedPath[0] = initialPosition;
				for(int i = 1; i < midPointsConverted.Length + 1; i++)
				{
					GeneratedPath[i] = midPointsConverted[i - 1];
					PathDistance += (GeneratedPath[i] - GeneratedPath[i - 1]).magnitude;
				}
				GeneratedPath[GeneratedPath.Length - 1] = finalPosition;

				//One last segement to calc
				PathDistance += (GeneratedPath[GeneratedPath.Length - 1] - GeneratedPath[GeneratedPath.Length - 2]).magnitude;
			}
			else
			{
				PathDistance = (finalPosition - initialPosition).magnitude;
				GeneratedPath = new Vector3[1 + 1] { initialPosition, finalPosition };
			}
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			if (TotalLengthDuration == 0) //this case can happen there is no diff between last and first point.
			{
				StopGenerator();
				SetFinalRotation(entity);

				return entity.transform.position = GeneratedPath[GeneratedPath.Length - 1]; //this is fine, it's ALWAYS at least 2 in size.
			}
			else
			{
				//We ADD current time to it because the ctor may have added a negative offset.
				StartTimeStamp += currentTime;

				//Calculate the ending timestamp as the total duration by SPEED and convert to milliseconds.
				EndTimeStamp = ((long)(TotalLengthDuration) + StartTimeStamp);

				//TODO: Is this a hack, to rewrite initial position??
				if (Vector3.Distance(GeneratedPath[0], entity.transform.position) < 4.0f)
				{
					PathDistance += Vector3.Distance(GeneratedPath[0], entity.transform.position);
					GeneratedPath[0] = entity.transform.position;
				}

				//meters per millisecond
				MovementSpeedModifier = PathDistance / TotalLengthDuration * 1000.0f;

				State = new LinearPointPathState(GeneratedPath[0], GeneratedPath[1], CalculateMovementSpeed(), 0, (int) StartTimeStamp);
			}

			return entity.transform.position;
		}

		protected virtual void SetFinalRotation([NotNull] GameObject entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			Vector3 direction = GeneratedPath[GeneratedPath.Length - 1] - GeneratedPath[GeneratedPath.Length - 2];
			direction = new Vector3(direction.x, 0.0f, direction.z);

			entity.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}

		private float CalculateMovementSpeed()
		{
			//TODO: When do creatures walk??
			return MovementSpeedModifier;
			//return MovementSpeedCollection[UnitMoveType.MOVE_RUN];
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			if (EndTimeStamp <= currentTime)
			{
				//It's past time, let's STOP
				StopGenerator();
				SetFinalRotation(entity);

				return entity.transform.position = GeneratedPath.Last();
			}

			int diffSinceLastPoint = (int)currentTime - State.StartTimeStamp;

			//Possible first tick has no diff, and we can skip
			if (diffSinceLastPoint == 0)
				return entity.transform.position;

#if DEBUG
			for (int i = 0; i < GeneratedPath.Length - 1; i++)
			{
				Debug.DrawRay(GeneratedPath[i], (GeneratedPath[i] - GeneratedPath[i + 1]), Color.white);
			}
#endif

			//X is total time required to reach this point. X - 1 is the time elapsed to reach the starting point of this path.
			//from that we can get the duration this segment should take and divide it by the total miliseconds elapased since start of segment.
			float segmentCompleteRatio = diffSinceLastPoint / (float)State.MillisecondsRequiredUntilNextPoint;

			//TODO: Optimize
			Vector3 direction = (GeneratedPath[State.CurrentIndex + 1] - GeneratedPath[State.CurrentIndex]);
			direction = new Vector3(direction.x, 0.0f, direction.z);

			InterpolateRotation(entity, direction);
			entity.transform.position = Vector3.Lerp(GeneratedPath[State.CurrentIndex], GeneratedPath[State.CurrentIndex + 1], Mathf.Clamp(segmentCompleteRatio, 0, 1.0f));

#if DEBUG
			Debug.DrawRay(entity.transform.position, direction * 3.0f, Color.blue);
#endif

			//We're at the end
			if (segmentCompleteRatio >= 1.0f)
			{
				if (State.CurrentIndex + 2 == GeneratedPath.Length)
				{
					StopGenerator();
					SetFinalRotation(entity);
				}
				else
				{
					State = new LinearPointPathState(GeneratedPath[State.CurrentIndex + 1], GeneratedPath[State.CurrentIndex + 2], CalculateMovementSpeed(), State.CurrentIndex + 1, (int) currentTime);
				}
			}

			return entity.transform.position;
		}

		protected virtual void InterpolateRotation(GameObject entity, Vector3 direction)
		{
			entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 100.0f * Time.deltaTime);
		}
	}
}