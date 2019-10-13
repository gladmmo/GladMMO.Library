using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class BehaviourGameObjectState<TBehaviourType> : DefaultGameObjectActorState
		where TBehaviourType : class, IGameObjectLinkable
	{
		public TBehaviourType BehaviourData { get; }

		public BehaviourGameObjectState(IEntityDataFieldContainer entityData, NetworkEntityGuid entityGuid, GameObjectInstanceModel instanceModel, GameObjectTemplateModel templateModel,
			[NotNull] TBehaviourType behaviourData) 
			: base(entityData, entityGuid, instanceModel, templateModel)
		{
			BehaviourData = behaviourData ?? throw new ArgumentNullException(nameof(behaviourData));
		}
	}
}
