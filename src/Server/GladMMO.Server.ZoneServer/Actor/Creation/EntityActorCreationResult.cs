using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class EntityActorCreationResult
	{
		public Type DesiredActorType { get; }

		public IEntityActorStateInitializeMessage<IEntityActorStateContainable> InitializationMessage { get; }

		public EntityActorCreationResult([NotNull] Type desiredActorType, [NotNull] IEntityActorStateInitializeMessage<IEntityActorStateContainable> initializationMessage)
		{
			DesiredActorType = desiredActorType ?? throw new ArgumentNullException(nameof(desiredActorType));
			InitializationMessage = initializationMessage ?? throw new ArgumentNullException(nameof(initializationMessage));
		}
	}
}
