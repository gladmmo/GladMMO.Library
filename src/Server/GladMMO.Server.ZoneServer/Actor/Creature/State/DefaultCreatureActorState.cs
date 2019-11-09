using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultCreatureActorState : NetworkedObjectActorState
	{
		public CreatureInstanceModel InstanceModel { get; }

		public CreatureTemplateModel TemplateModel { get; }

		public DefaultCreatureActorState([NotNull] IEntityDataFieldContainer entityData,
			[NotNull] NetworkEntityGuid entityGuid,
			[NotNull] CreatureInstanceModel instanceModel,
			[NotNull] CreatureTemplateModel templateModel,
			[NotNull] InterestCollection interest)
			: base(entityData, entityGuid, interest)
		{
			InstanceModel = instanceModel ?? throw new ArgumentNullException(nameof(instanceModel));
			TemplateModel = templateModel ?? throw new ArgumentNullException(nameof(templateModel));
		}
	}
}
