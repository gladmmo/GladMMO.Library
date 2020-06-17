using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	public interface IMovementDataUpdater<in TDataType>
	{
		/// <summary>
		/// Handles the update logic for a <typeparamref name="TDataType"/> update.
		/// </summary>
		/// <param name="guid">The entity guid.</param>
		/// <param name="data">The data to handle for updating.</param>
		/// <param name="updateComponent">Indicates if the <see cref="data"/> should be updated/added as a component to the Entity Component system.</param>
		void Update(ObjectGuid guid, TDataType data, bool updateComponent);
	}

	public sealed class DefaultMovementDataUpdater : IMovementDataUpdater<MovementBlockData>
	{
		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IEntityGuidMappable<SplineInfo> SplineInfoMappable { get; }

		public DefaultMovementDataUpdater([NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> movementGeneratorFactory,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IEntityGuidMappable<SplineInfo> splineInfoMappable)
		{
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			SplineInfoMappable = splineInfoMappable ?? throw new ArgumentNullException(nameof(splineInfoMappable));
		}

		public void Update(ObjectGuid guid, MovementBlockData data, bool updateComponent)
		{
			if (updateComponent)
				MovementDataMappable[guid] = data;

			//We're dead, an unmoving corpse.
			if(data.IsDead)
			{
				MovementGeneratorMappable[guid] = new IdleMovementGenerator(data.MoveInfo.Position.ToUnityVector(), data.MoveInfo.Orientation.ToUnity3DYAxisRotation());
				return;
			}
			else
			{
				//Maybe we have a spline
				if(data.HasSplineData)
					SplineInfoMappable[guid] = data.SplineInformation; //we may already have it, we cannot add.

				IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<MovementInfo>(guid, data.MoveInfo));
				MovementGeneratorMappable[guid] = generator;
			}
		}
	}
}
