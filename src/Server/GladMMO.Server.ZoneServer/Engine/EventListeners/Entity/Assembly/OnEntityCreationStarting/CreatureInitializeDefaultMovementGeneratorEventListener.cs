using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class CreatureInitializeDefaultMovementGeneratorEventListener : CreatureCreationStartingEventListener
	{
		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IReadonlyEntityGuidMappable<CreatureInstanceModel> CreatureInstanceModel { get; }

		public CreatureInitializeDefaultMovementGeneratorEventListener(IEntityCreationStartingEventSubscribable subscriptionService, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<CreatureInstanceModel> creatureInstanceModel) 
			: base(subscriptionService)
		{
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			CreatureInstanceModel = creatureInstanceModel ?? throw new ArgumentNullException(nameof(creatureInstanceModel));
		}

		protected override void OnCreatureEntityCreationStarting(EntityCreationStartingEventArgs args)
		{
			//We do nothing if something has already initialized the movement.
			if (MovementDataMappable.ContainsKey(args.EntityGuid))
				return;

			CreatureInstanceModel instanceModel = CreatureInstanceModel.RetrieveEntity(args.EntityGuid);

			MovementDataMappable.AddObject(args.EntityGuid, new PositionChangeMovementData(0, instanceModel.InitialPosition, Vector2.zero, instanceModel.YAxisRotation));
		}
	}
}
