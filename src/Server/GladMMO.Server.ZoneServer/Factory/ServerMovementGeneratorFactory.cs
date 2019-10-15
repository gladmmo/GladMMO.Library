using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IMovementGeneratorFactory : IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>>
	{

	}

	public sealed class ServerMovementGeneratorFactory : IMovementGeneratorFactory
	{
		private IReadonlyEntityGuidMappable<CharacterController> ControllerMappable { get; }

		public ServerMovementGeneratorFactory([NotNull] IReadonlyEntityGuidMappable<CharacterController> controllerMappable)
		{
			ControllerMappable = controllerMappable ?? throw new ArgumentNullException(nameof(controllerMappable));
		}

		public IMovementGenerator<GameObject> Create(EntityAssociatedData<IMovementData> context)
		{
			//TODO: Another temporary hack
			if (context.EntityGuid.EntityType == EntityType.Creature || context.EntityGuid.EntityType == EntityType.GameObject)
			{
				if(context.Data is PositionChangeMovementData d)
					if(d.Direction == Vector2.zero)
						return new IdleMovementGenerator(context.Data.InitialPosition);
					else
						throw new InvalidOperationException($"Cannot create Creature/GameObject Movement for Type: {context.Data.GetType().Name} For Entity: {context.EntityGuid}");

				if (context.Data is PathBasedMovementData pathData)
					return new PathMovementGenerator(pathData);
			}

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
