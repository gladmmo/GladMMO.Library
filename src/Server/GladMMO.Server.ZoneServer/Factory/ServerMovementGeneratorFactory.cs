using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class ServerMovementGeneratorFactory : IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>>
	{
		private IReadonlyEntityGuidMappable<CharacterController> ControllerMappable { get; }

		public ServerMovementGeneratorFactory([NotNull] IReadonlyEntityGuidMappable<CharacterController> controllerMappable)
		{
			ControllerMappable = controllerMappable ?? throw new ArgumentNullException(nameof(controllerMappable));
		}

		public IMovementGenerator<GameObject> Create(EntityAssociatedData<IMovementData> context)
		{
			//TODO: Another temporary hack
			if(context.EntityGuid.EntityType == EntityType.Creature)
				return new IdleMovementGenerator(context.Data.InitialPosition);

			//TODO: redo all this of this garbage
			if (context.Data is PositionChangeMovementData pcmd)
				return new ClientCharacterControllerInputMovementGenerator(pcmd, BuildLazyControllerFactory(context));

			throw new NotSupportedException($"TODO: Encountered unsupported movement data: {context.Data.GetType().Name}");
		}

		private Lazy<CharacterController> BuildLazyControllerFactory([NotNull] EntityAssociatedData<IMovementData> context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			return new Lazy<CharacterController>(() => ControllerMappable.RetrieveEntity(context.EntityGuid));
		}
	}
}
