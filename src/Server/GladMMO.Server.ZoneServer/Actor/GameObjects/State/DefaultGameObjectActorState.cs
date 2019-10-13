using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultGameObjectActorState : IEntityActorStateContainable
	{
		public IEntityDataFieldContainer EntityData { get; }

		public NetworkEntityGuid EntityGuid { get; }

		public GameObjectInstanceModel InstanceModel { get; }

		public GameObjectTemplateModel TemplateModel { get; }

		public DefaultGameObjectActorState([NotNull] IEntityDataFieldContainer entityData,
			[NotNull] NetworkEntityGuid entityGuid,
			[NotNull] GameObjectInstanceModel instanceModel,
			[NotNull] GameObjectTemplateModel templateModel)
		{
			EntityData = entityData ?? throw new ArgumentNullException(nameof(entityData));
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			InstanceModel = instanceModel ?? throw new ArgumentNullException(nameof(instanceModel));
			TemplateModel = templateModel ?? throw new ArgumentNullException(nameof(templateModel));
		}
	}
}
