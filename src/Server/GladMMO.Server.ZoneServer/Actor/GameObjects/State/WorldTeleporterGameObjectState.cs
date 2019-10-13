using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class WorldTeleporterGameObjectState : DefaultGameObjectActorState
	{
		public WorldTeleporterInstanceModel BehaviourData { get; }

		public WorldTeleporterGameObjectState(IEntityDataFieldContainer entityData, NetworkEntityGuid entityGuid, GameObjectInstanceModel instanceModel, GameObjectTemplateModel templateModel,
			[NotNull] WorldTeleporterInstanceModel behaviourData) 
			: base(entityData, entityGuid, instanceModel, templateModel)
		{
			BehaviourData = behaviourData ?? throw new ArgumentNullException(nameof(behaviourData));
		}
	}
}
