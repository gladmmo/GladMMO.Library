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
		protected override Vector3 Start(GameObject entity, long currentTime)
		{
			//Let's do nothing!
			return entity.transform.position;
		}

		protected override Vector3 InternalUpdate(GameObject entity, long currentTime)
		{
			//Let's do nothing, forever!
			return entity.transform.position;
		}
	}
}
