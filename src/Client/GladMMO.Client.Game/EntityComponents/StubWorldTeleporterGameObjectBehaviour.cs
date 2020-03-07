using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class StubWorldTeleporterGameObjectBehaviour : BaseClientGameObjectEntityBehaviourComponent
	{
		public StubWorldTeleporterGameObjectBehaviour(ObjectGuid targetEntity, 
			GameObject rootSceneObject, 
			IEntityDataFieldContainer data) 
			: base(targetEntity, rootSceneObject, data)
		{

		}
	}
}
