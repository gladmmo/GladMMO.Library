using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientInitializeDefaultMovementGeneratorEventListener : SharedCreatingInitializeDefaultMovementGeneratorEventListener
	{
		public ClientInitializeDefaultMovementGeneratorEventListener(IEntityCreationFinishedEventSubscribable subscriptionService, 
			IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable, 
			IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> movementGeneratorFactory, 
			IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable) 
			: base(subscriptionService, movementGeneratorMappable, movementGeneratorFactory, movementDataMappable)
		{

		}
	}
}
