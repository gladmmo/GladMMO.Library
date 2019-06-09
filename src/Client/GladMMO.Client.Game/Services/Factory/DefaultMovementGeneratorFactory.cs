using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: This is a WIP. It does not support movement generator creation will. It does not really support anything but players.
	public sealed class DefaultMovementGeneratorFactory : IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>>
	{
		private IReadonlyEntityGuidMappable<CharacterController> ControllerMappable { get; }

		public DefaultMovementGeneratorFactory([NotNull] IReadonlyEntityGuidMappable<CharacterController> controllerMappable)
		{
			ControllerMappable = controllerMappable ?? throw new ArgumentNullException(nameof(controllerMappable));
		}

		public IMovementGenerator<GameObject> Create(EntityAssociatedData<IMovementData> context)
		{
			//TODO: redo all this of this garbage
			if (context.Data is PositionChangeMovementData pcmd)
			{
				//The reason we use a lazy here is because we can't promise that the character controller exists AT ALL at this point sadly.
				return new ClientCharacterControllerInputMovementGenerator(pcmd, new Lazy<CharacterController>(() => ControllerMappable.RetrieveEntity(context.EntityGuid)));
			}

			throw new NotSupportedException($"TODO: Encountered unsupported movement data: {context.Data.GetType().Name}");
		}
	}
}
