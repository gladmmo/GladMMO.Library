using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnEntityCreatingInitializeDefaultMovementGeneratorEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		public OnEntityCreatingInitializeDefaultMovementGeneratorEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> movementGeneratorFactory,
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementDataMappable) 
			: base(subscriptionService)
		{
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			IMovementData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<IMovementData>(args.EntityGuid, movementData));
			MovementGeneratorMappable.AddObject(args.EntityGuid, generator);
		}
	}
}
