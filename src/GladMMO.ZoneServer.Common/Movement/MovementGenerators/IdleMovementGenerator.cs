using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// The do nothing no good movement generator.
	/// </summary>
	public sealed class IdleMovementGenerator : MoveGenerator
	{
		protected override void Start(GameObject entity, long currentTime)
		{
			//Let's do nothing!
		}

		protected override void InternalUpdate(GameObject entity, long currentTime)
		{
			//Let's do nothing, forever!
		}
	}
}
