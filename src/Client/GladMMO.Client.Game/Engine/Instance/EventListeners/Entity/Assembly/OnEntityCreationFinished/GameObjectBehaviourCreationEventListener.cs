using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GameObjectBehaviourCreationEventListener : GameObjectCreationFinishedEventListener
	{
		private IClientGameObjectEntityBehaviourFactory BehaviourFactory { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public GameObjectBehaviourCreationEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IClientGameObjectEntityBehaviourFactory behaviourFactory,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable) 
			: base(subscriptionService)
		{
			BehaviourFactory = behaviourFactory ?? throw new ArgumentNullException(nameof(behaviourFactory));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			IEntityDataFieldContainer dataContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);

			switch (dataContainer.GetFieldValue<GameObjectType>(GameObjectField.GAMEOBJECT_TYPE_ID))
			{
				//Visual doesn't have behaviours
				case GameObjectType.Visual:
					return;
				default:
				{
					CreateBehaviourComponent(args.EntityGuid);
				}
					break;
			}
		}

		private void CreateBehaviourComponent(NetworkEntityGuid entityGuid)
		{
			BaseEntityBehaviourComponent behaviourComponent = BehaviourFactory.Create(entityGuid);

			if (behaviourComponent is IBehaviourComponentInitializable init)
				init.Initialize();
		}
	}
}
