using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
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

		private IEntityGuidMappable<EntityMovementSpeed> MovementSpeedMappable { get; }

		private IEntityGuidMappable<SplineInfo> SplineInfoMappable { get; }

		private ILog Logger { get; }

		public DefaultMovementDataUpdater([NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> movementGeneratorFactory,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IEntityGuidMappable<SplineInfo> splineInfoMappable,
			[NotNull] ILog logger,
			[NotNull] IEntityGuidMappable<EntityMovementSpeed> movementSpeedMappable)
		{
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			SplineInfoMappable = splineInfoMappable ?? throw new ArgumentNullException(nameof(splineInfoMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			MovementSpeedMappable = movementSpeedMappable ?? throw new ArgumentNullException(nameof(movementSpeedMappable));
		}

		public void Update(ObjectGuid guid, MovementBlockData data, bool updateComponent)
		{
			if (updateComponent)
			{
				MovementDataMappable[guid] = data;

				//Movement speed only exists for living creatures
				if (data.IsLiving)
				{
					//Special handling for mutable component type.
					if(MovementSpeedMappable.ContainsKey(guid))
						MovementSpeedMappable[guid].SetMovementSpeeds(data.MovementSpeeds);
					else
						MovementSpeedMappable[guid] = new EntityMovementSpeed(data.MovementSpeeds);
				}
			}
				

			//TODO: Handle cases where JUST POS and JUST orientation are sent and other unhandled cases.
			if(data.IsLiving) //IsLiving is required for MoveInfo which is required here for movement generator factory.
			{
				//Maybe we have a spline
				if(data.HasSplineData)
					SplineInfoMappable[guid] = data.SplineInformation; //we may already have it, we cannot add.

				IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<MovementInfo>(guid, data.MoveInfo));
				MovementGeneratorMappable[guid] = generator;
			}
			else if (data.IsStationaryObject)
			{
				StationaryMovementInfo stationaryMovementInfo = data.StationaryObjectMovementInformation;
				Vector3 position = stationaryMovementInfo.Position.ToUnityVector();
				float orientation = stationaryMovementInfo.Orientation.ToUnity3DYAxisRotation();

				MovementGeneratorMappable[guid] = new IdleMovementGenerator(position, orientation);
			}
			else if (data.HasUpdatePosition && data.IsDead) //HasUpdatePosition and IsStationaryObject is mutually exclusive
			{
				//GameObjects can have this type of movement flags set
				//if they don't move.
				CorpseInfo deadMoveInfo = data.DeadMovementInformation;
				Vector3 position = deadMoveInfo.GoLocation.ToUnityVector();
				float orientation = deadMoveInfo.Orientation.ToUnity3DYAxisRotation();

				MovementGeneratorMappable[guid] = new IdleMovementGenerator(position, orientation);
			}
			else
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Failed to create MoveGenerator For: {guid} MoveFlags: {data.UpdateFlags}.");
			}
		}
	}
}
