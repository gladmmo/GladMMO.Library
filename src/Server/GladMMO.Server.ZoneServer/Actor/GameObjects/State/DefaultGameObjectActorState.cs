using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultGameObjectActorState : NetworkedObjectActorState
	{
		public GameObjectInstanceModel InstanceModel { get; }

		public GameObjectTemplateModel TemplateModel { get; }

		public DefaultGameObjectActorState([NotNull] IEntityDataFieldContainer entityData,
			[NotNull] NetworkEntityGuid entityGuid,
			[NotNull] GameObjectInstanceModel instanceModel,
			[NotNull] GameObjectTemplateModel templateModel,
			[NotNull] InterestCollection interest)
			: base(entityData, entityGuid, interest)
		{
			
			InstanceModel = instanceModel ?? throw new ArgumentNullException(nameof(instanceModel));
			TemplateModel = templateModel ?? throw new ArgumentNullException(nameof(templateModel));
		}
	}
}
