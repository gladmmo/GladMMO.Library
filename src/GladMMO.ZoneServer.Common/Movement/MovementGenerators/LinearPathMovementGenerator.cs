using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	public class LinearPathMovementGenerator : BaseMovementGenerator<LinearPathMoveInfo>
	{
		protected Vector3[] GeneratedPath { get; }

		/// <summary>
		/// Represents the amount of milliseconds required to traverse from <see cref="GeneratedPath"/> X to <see cref="GeneratedPath"/> X + 1.
		/// </summary>
		protected int[] TimeWeights { get; }

		protected int TotalLengthDuration { get; }

		protected long StartTimeStamp { get; set; }

		protected long EndTimeStamp { get; set; }

		public LinearPathMovementGenerator(LinearPathMoveInfo movementData, Vector3 initialPosition, int totalLengthDuration) 
			: this(movementData, initialPosition, totalLengthDuration, 0)
		{
			
		}

		public LinearPathMovementGenerator(LinearPathMoveInfo movementData, Vector3 initialPosition, int totalLengthDuration, int timeElapsed)
			: base(movementData, initialPosition)
		{
			if(totalLengthDuration < 0) throw new ArgumentOutOfRangeException(nameof(totalLengthDuration));

			TotalLengthDuration = totalLengthDuration;

			//Push the Start timestamp backwards, since some splines we may see start in the middle.
			StartTimeStamp -= timeElapsed;

			//TODO: support middle points
			GeneratedPath = new Vector3[1 + 1] { initialPosition, movementData.FinalPosition.ToUnityVector() };
			TimeWeights = new int[GeneratedPath.Length - 1];

			//TODO: Don't hardcode speed.
			//TODO: Calculate weighted time between points
			for(int i = 0; i < GeneratedPath.Length - 1; i++)
				TimeWeights[i] = (int)(1000 * Vector3.Distance(GeneratedPath[i], GeneratedPath[i + 1]) / 8.0f); //Creatures default speed is 8.0 yards a second.

			//Now add the previous index weights
			for(int i = 1; i < TimeWeights.Length; i++)
				TimeWeights[i] = TimeWeights[i - 1];
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			if (TotalLengthDuration == 0 || TimeWeights[0] == 0) //this case can happen there is no diff between last and first point.
			{
				StopGenerator();

				return entity.transform.position = GeneratedPath[GeneratedPath.Length - 1]; //this is fine, it's ALWAYS at least 2 in size.
			}
			else
			{
				//We ADD current time to it because the ctor may have added a negative offset.
				StartTimeStamp += currentTime;

				//Calculate the ending timestamp as the total duration by SPEED and convert to milliseconds.
				EndTimeStamp = ((long)(TotalLengthDuration) + StartTimeStamp);

				//TODO: Is this a hack, to rewrite initial position??
				if(Vector3.Distance(GeneratedPath[0], entity.transform.position) < 4.0f)
					GeneratedPath[0] = entity.transform.position;
			}

			return entity.transform.position;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			if(EndTimeStamp <= currentTime)
				return entity.transform.position = GeneratedPath.Last();

			int millisecondsTotalDiff = (int) (currentTime - StartTimeStamp);

			//Possible first tick has no diff, and we can skip
			if (millisecondsTotalDiff == 0)
				return entity.transform.position;

#if DEBUG
			Debug.DrawRay(entity.transform.position, (entity.transform.position - GeneratedPath[0]), Color.white, 2.0f);
			for (int i = 0; i < GeneratedPath.Length - 1; i++)
			{
				Debug.DrawRay(GeneratedPath[i], (GeneratedPath[i] - GeneratedPath[i + 1]), Color.white, 2.0f);
			}
#endif

			//Find the point we're on
			for (int i = 0; i < TimeWeights.Length; i++)
			{
				//If we have pasted this point path
				if(TimeWeights[i] < millisecondsTotalDiff)
					continue;
				else
				{
					//X is total time required to reach this point. X - 1 is the time elapsed to reach the starting point of this path.
					//from that we can get the duration this segment should take and divide it by the total miliseconds elapased since start of segment.
					float segmentCompleteRatio = i == 0 ? (float)millisecondsTotalDiff / TimeWeights[i] :  (float)(millisecondsTotalDiff - TimeWeights[i - 1]) / (TimeWeights[i] - TimeWeights[i - 1]);

					//TODO: Optimize
					Vector3 direction = (GeneratedPath[i + 1] - GeneratedPath[i]);
					direction = new Vector3(direction.x, 0.0f, direction.z);

					entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 100.0f * Time.deltaTime);
					entity.transform.position = Vector3.Lerp(GeneratedPath[i], GeneratedPath[i + 1], Mathf.Clamp(segmentCompleteRatio, 0, 1.0f));

#if DEBUG
					Debug.DrawRay(entity.transform.position, direction * 3.0f, Color.blue);
#endif
				}
			}

			return entity.transform.position;
		}
	}
}
