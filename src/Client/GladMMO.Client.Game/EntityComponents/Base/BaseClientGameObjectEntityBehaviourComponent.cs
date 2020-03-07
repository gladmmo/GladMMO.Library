using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public abstract class BaseClientGameObjectEntityBehaviourComponent : BaseEntityBehaviourComponent
	{
		protected GameObject RootSceneObject { get; }

		//TODO: We should move this to base class, server objects should have access to their data too.
		protected IEntityDataFieldContainer Data { get; }

		public BaseClientGameObjectEntityBehaviourComponent(ObjectGuid targetEntity, 
			[NotNull] GameObject rootSceneObject,
			[NotNull] IEntityDataFieldContainer data) 
			: base(targetEntity)
		{
			RootSceneObject = rootSceneObject ?? throw new ArgumentNullException(nameof(rootSceneObject));
			Data = data ?? throw new ArgumentNullException(nameof(data));
		}
	}
}
