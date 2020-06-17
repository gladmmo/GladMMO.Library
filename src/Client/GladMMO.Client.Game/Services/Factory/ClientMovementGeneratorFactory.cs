﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: This is a WIP. It does not support movement generator creation will. It does not really support anything but players.
	public sealed class ClientMovementGeneratorFactory : IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>>
	{
		private IReadonlyEntityGuidMappable<CharacterController> ControllerMappable { get; }

		private ILocalPlayerDetails LocalPlayerDetails { get; }

		private IReadonlyEntityGuidMappable<SplineInfo> SplineInfoMappable { get; }

		public ClientMovementGeneratorFactory([NotNull] IReadonlyEntityGuidMappable<CharacterController> controllerMappable, 
			[NotNull] ILocalPlayerDetails localPlayerDetails,
			[NotNull] IReadonlyEntityGuidMappable<SplineInfo> splineInfoMappable)
		{
			ControllerMappable = controllerMappable ?? throw new ArgumentNullException(nameof(controllerMappable));
			LocalPlayerDetails = localPlayerDetails ?? throw new ArgumentNullException(nameof(localPlayerDetails));
			SplineInfoMappable = splineInfoMappable ?? throw new ArgumentNullException(nameof(splineInfoMappable));
		}

		public IMovementGenerator<GameObject> Create(EntityAssociatedData<MovementInfo> context)
		{
			switch (context.EntityGuid.TypeId)
			{
				case EntityTypeId.TYPEID_PLAYER:
					return CreatePlayerMovementGenerator(context);
				case EntityTypeId.TYPEID_GAMEOBJECT:
					//TODO: Support non-static GameObjects.
					return new IdleMovementGenerator(context.Data.Position.ToUnityVector(), context.Data.Orientation.ToUnity3DYAxisRotation());
				case EntityTypeId.TYPEID_UNIT:
					if (context.Data.MoveFlags.HasAnyFlags(MovementFlag.MOVEMENTFLAG_SPLINE_ENABLED))
					{
						SplineInfo info = SplineInfoMappable.RetrieveEntity(context.EntityGuid);
						//TODO: Support cyclical
						//TODO: Fix linear path
						LinearPathMoveInfo spoofedLinearMoveInfo = new LinearPathMoveInfo(info.WayPoints.Length - 1, info.SplineEndpoint, Array.Empty<Vector3<float>>());
						return new LinearPathMovementGenerator(spoofedLinearMoveInfo, context.Data.Position.ToUnityVector(), info.SplineFullTime, info.SplineTime);
					}
					else
						return new IdleMovementGenerator(context.Data.Position.ToUnityVector(), context.Data.Orientation.ToUnity3DYAxisRotation());
				default:
					throw new ArgumentOutOfRangeException();
			}

			throw new NotSupportedException($"TODO: Encountered unsupported movement data: {context.Data.GetType().Name}");
		}

		private IMovementGenerator<GameObject> CreatePlayerMovementGenerator(EntityAssociatedData<MovementInfo> context)
		{
			//The reason we use a lazy here is because we can't promise that the character controller exists AT ALL at this point sadly.
			return new ClientCharacterControllerInputMovementGenerator(context.Data, BuildLazyControllerFactory(context), LocalPlayerDetails.LocalPlayerGuid == context.EntityGuid);
		}

		private Lazy<CharacterController> BuildLazyControllerFactory(EntityAssociatedData<MovementInfo> context)
		{
			return new Lazy<CharacterController>(() => ControllerMappable.RetrieveEntity(context.EntityGuid));
		}
	}
}
