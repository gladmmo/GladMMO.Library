using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultCreatureActorState : DefaultEntityActorStateContainer
	{
		public CreatureInstanceModel InstanceModel { get; }

		public CreatureTemplateModel TemplateModel { get; }

		public DefaultCreatureActorState([NotNull] IEntityDataFieldContainer entityData,
			[NotNull] NetworkEntityGuid entityGuid,
			[NotNull] CreatureInstanceModel instanceModel,
			[NotNull] CreatureTemplateModel templateModel)
			: base(entityData, entityGuid)
		{
			InstanceModel = instanceModel ?? throw new ArgumentNullException(nameof(instanceModel));
			TemplateModel = templateModel ?? throw new ArgumentNullException(nameof(templateModel));
		}
	}
}
