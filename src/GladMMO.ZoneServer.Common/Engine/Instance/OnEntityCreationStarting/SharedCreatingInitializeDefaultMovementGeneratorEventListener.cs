using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Fasterflect;
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
		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IEntityGuidMappable<SplineInfo> SplineInfoMappable { get; }

		public SharedCreatingInitializeDefaultMovementGeneratorEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> movementGeneratorFactory,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IEntityGuidMappable<SplineInfo> splineInfoMappable) 
			: base(subscriptionService)
		{
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			SplineInfoMappable = splineInfoMappable ?? throw new ArgumentNullException(nameof(splineInfoMappable));
		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			//Non-world objects don't have move generators.
			if (!args.EntityGuid.IsWorldObject())
				return;

			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			//We're dead, an unmoving corpse.
			if (movementData.IsDead)
			{
				MovementGeneratorMappable.AddObject(args.EntityGuid, new IdleMovementGenerator(movementData.MoveInfo.Position.ToUnityVector()));
				return;
			}
			else
			{
				//Maybe we have a spline
				if(movementData.HasSplineData)
					SplineInfoMappable.AddObject(args.EntityGuid, movementData.SplineInformation);

				IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<MovementInfo>(args.EntityGuid, movementData.MoveInfo));
				MovementGeneratorMappable.AddObject(args.EntityGuid, generator);
			}
		}
	}
}
