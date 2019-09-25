using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeCreatureModelEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityGuidMappable<CreatureTemplateModel> CreatureTemplateMappable { get; }

		public InitializeCreatureModelEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IEntityGuidMappable<CreatureTemplateModel> creatureTemplateMappable) 
			: base(subscriptionService)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			CreatureTemplateMappable = creatureTemplateMappable ?? throw new ArgumentNullException(nameof(creatureTemplateMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//Only do creatures.
			if (args.EntityGuid.EntityType != EntityType.Creature)
				return;

			IEntityDataFieldContainer dataContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);
			CreatureTemplateModel template = CreatureTemplateMappable.RetrieveEntity(args.EntityGuid);

			//Initialize the creature display model ID.
			dataContainer.SetFieldValue(EUnitFields.UNIT_FIELD_DISPLAYID, template.ModelId);
		}
	}
}
