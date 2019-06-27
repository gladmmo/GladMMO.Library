using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ServerCreatingInitializeDefaultMovementGeneratorEventListener : SharedCreatingInitializeDefaultMovementGeneratorEventListener
	{
		public ServerCreatingInitializeDefaultMovementGeneratorEventListener(IEntityCreationStartingEventSubscribable subscriptionService, IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable, IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> movementGeneratorFactory, IReadonlyEntityGuidMappable<IMovementData> movementDataMappable) : base(subscriptionService, movementGeneratorMappable, movementGeneratorFactory, movementDataMappable)
		{

		}
	}
}
