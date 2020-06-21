using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IPhysicsTrigger
	{
		void OnTriggerEnter(Collider enteredCollider);

		void OnTriggerExit(Collider exitedCollider);
	}
}
