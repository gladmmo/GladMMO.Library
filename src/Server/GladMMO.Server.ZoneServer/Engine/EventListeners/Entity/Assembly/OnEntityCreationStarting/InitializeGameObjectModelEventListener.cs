using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeGameObjectModelEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityGuidMappable<GameObjectTemplateModel> GameObjectTemplateMappable { get; }

		public InitializeGameObjectModelEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IEntityGuidMappable<GameObjectTemplateModel> gameObjectTemplateMappable) 
			: base(subscriptionService)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			GameObjectTemplateMappable = gameObjectTemplateMappable ?? throw new ArgumentNullException(nameof(gameObjectTemplateMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//Only do creatures.
			if (args.EntityGuid.EntityType != EntityType.GameObject)
				return;

			IEntityDataFieldContainer dataContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);
			GameObjectTemplateModel template = GameObjectTemplateMappable.RetrieveEntity(args.EntityGuid);

			//Initialize the creature display model ID.
			dataContainer.SetFieldValue(EUnitFields.UNIT_FIELD_DISPLAYID, template.ModelId);
		}
	}
}
