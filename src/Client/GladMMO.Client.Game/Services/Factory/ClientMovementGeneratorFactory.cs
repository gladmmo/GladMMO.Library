﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: This is a WIP. It does not support movement generator creation will. It does not really support anything but players.
	public sealed class ClientMovementGeneratorFactory : IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementBlockData>>
	{
		private IReadonlyEntityGuidMappable<CharacterController> ControllerMappable { get; }

		private ILocalPlayerDetails LocalPlayerDetails { get; }

		public ClientMovementGeneratorFactory([NotNull] IReadonlyEntityGuidMappable<CharacterController> controllerMappable, [NotNull] ILocalPlayerDetails localPlayerDetails)
		{
			ControllerMappable = controllerMappable ?? throw new ArgumentNullException(nameof(controllerMappable));
			LocalPlayerDetails = localPlayerDetails ?? throw new ArgumentNullException(nameof(localPlayerDetails));
		}

		public IMovementGenerator<GameObject> Create(EntityAssociatedData<MovementBlockData> context)
		{
			switch (context.EntityGuid.TypeId)
			{
				case EntityTypeId.TYPEID_PLAYER:
					return CreatePlayerMovementGenerator(context);
				case EntityTypeId.TYPEID_GAMEOBJECT:
					//TODO: Support non-static GameObjects.
					return new IdleMovementGenerator(context.Data.MoveInfo.Position.ToUnityVector());
				case EntityTypeId.TYPEID_UNIT:
					//TODO: Support non-static NPCs.
					return CreateCreatureMovementGenerator(context);
				default:
					throw new ArgumentOutOfRangeException();
			}

			throw new NotSupportedException($"TODO: Encountered unsupported movement data: {context.Data.GetType().Name}");
		}

		private static IMovementGenerator<GameObject> CreateCreatureMovementGenerator(EntityAssociatedData<MovementBlockData> context)
		{
			throw new InvalidOperationException($"Recieved unhandled Movement Type: {context.Data.GetType().Name} for Creature: {context.EntityGuid}.");
		}

		private IMovementGenerator<GameObject> CreatePlayerMovementGenerator(EntityAssociatedData<MovementBlockData> context)
		{
			//The reason we use a lazy here is because we can't promise that the character controller exists AT ALL at this point sadly.
			return new ClientCharacterControllerInputMovementGenerator(context.Data, BuildLazyControllerFactory(context), LocalPlayerDetails.LocalPlayerGuid != context.EntityGuid);

			throw new NotSupportedException($"TODO: Encountered unsupported movement data: {context.Data.GetType().Name}");
		}

		private Lazy<CharacterController> BuildLazyControllerFactory(EntityAssociatedData<MovementBlockData> context)
		{
			return new Lazy<CharacterController>(() => ControllerMappable.RetrieveEntity(context.EntityGuid));
		}
	}
}
