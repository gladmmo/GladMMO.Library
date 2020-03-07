using System; using FreecraftCore;
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

			throw new NotImplementedException($"TODO: Maybe rehandle gameobject local scripts");
			/*switch (dataContainer.GetEnumFieldValue<GameObjectType>(EGameObjectFields.GAMEOBJECT_TYPE_ID))
			{
				//Visual doesn't have behaviours
				case GameObjectType.Visual:
					return;
				default:
					CreateBehaviourComponent(args.EntityGuid);
					break;
			}*/
		}

		private void CreateBehaviourComponent(ObjectGuid entityGuid)
		{
			BaseEntityBehaviourComponent behaviourComponent = BehaviourFactory.Create(entityGuid);

			if (behaviourComponent is IBehaviourComponentInitializable init)
				init.Initialize();
		}
	}
}
