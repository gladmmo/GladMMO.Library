using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class GameObjectInitializeDefaultMovementGeneratorEventListener : GameObjectCreationStartingEventListener
	{
		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IReadonlyEntityGuidMappable<GameObjectInstanceModel> GameObjectInstanceModelMappable { get; }

		public GameObjectInitializeDefaultMovementGeneratorEventListener(IEntityCreationStartingEventSubscribable subscriptionService, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<GameObjectInstanceModel> gameObjectInstanceModel) 
			: base(subscriptionService)
		{
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			GameObjectInstanceModelMappable = gameObjectInstanceModel ?? throw new ArgumentNullException(nameof(gameObjectInstanceModel));
		}

		protected override void OnGameObjectEntityCreationStarting(EntityCreationStartingEventArgs args)
		{
			//We do nothing if something has already initialized the movement.
			if (MovementDataMappable.ContainsKey(args.EntityGuid))
				return;

			GameObjectInstanceModel instanceModel = GameObjectInstanceModelMappable.RetrieveEntity(args.EntityGuid);

			MovementDataMappable.AddObject(args.EntityGuid, new PositionChangeMovementData(0, instanceModel.InitialPosition, Vector2.zero, instanceModel.YAxisRotation));
		}
	}
}
