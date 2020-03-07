using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Base event listener that handles movement generator initialization
	/// for creating entities.
	/// </summary>
	public class SharedCreatingInitializeDefaultMovementGeneratorEventListener : BaseSingleEventListenerInitializable<IEntityCreationFinishedEventSubscribable, EntityCreationFinishedEventArgs>
	{
		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementBlockData>> MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		public SharedCreatingInitializeDefaultMovementGeneratorEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementBlockData>> movementGeneratorFactory,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable) 
			: base(subscriptionService)
		{
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<MovementBlockData>(args.EntityGuid, movementData));
			MovementGeneratorMappable.AddObject(args.EntityGuid, generator);
		}
	}
}
