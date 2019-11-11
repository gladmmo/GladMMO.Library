using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: Optimize idle to prevent Vector copying.
	/// <summary>
	/// The do nothing no good movement generator.
	/// </summary>
	public sealed class IdleMovementGenerator : MoveGenerator
	{
		private Vector3 InitialPosition { get; }

		public IdleMovementGenerator(Vector3 initialPosition)
		{
			InitialPosition = initialPosition;
		}

		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			//Immediately we're idle so let's stop the generator.
			StopGenerator();

			//Let's do nothing!
			return InitialPosition;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			//Let's do nothing, forever!
			return InitialPosition;
		}
	}
}
