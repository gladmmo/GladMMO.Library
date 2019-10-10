using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class StubWorldTeleporterGameObjectBehaviour : BaseClientGameObjectEntityBehaviourComponent
	{
		public StubWorldTeleporterGameObjectBehaviour(NetworkEntityGuid targetEntity, 
			GameObject rootSceneObject, 
			IEntityDataFieldContainer data) 
			: base(targetEntity, rootSceneObject, data)
		{

		}
	}
}
