using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class GameObjectDefaultFieldInitializationEventListener : GameObjectCreationFinishedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IReadonlyEntityGuidMappable<GameObjectTemplateModel> GameObjectTemplateMappable { get; }

		public GameObjectDefaultFieldInitializationEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<GameObjectTemplateModel> gameObjectTemplateMappable) 
			: base(subscriptionService)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			GameObjectTemplateMappable = gameObjectTemplateMappable ?? throw new ArgumentNullException(nameof(gameObjectTemplateMappable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			IEntityDataFieldContainer entityData = EntityDataMappable.RetrieveEntity(args.EntityGuid);
			GameObjectTemplateModel objectTemplateModel = GameObjectTemplateMappable.RetrieveEntity(args.EntityGuid);

			//We should generally consider this field static. I cannot even IMAGINE how we'd handle a change to this field on either
			//client or server.
			entityData.SetFieldValue(GameObjectField.GAMEOBJECT_TYPE_ID, objectTemplateModel.ObjectType);
		}
	}
}
