using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientInitializeDefaultMovementGeneratorEventListener : SharedCreatingInitializeDefaultMovementGeneratorEventListener
	{
		public ClientInitializeDefaultMovementGeneratorEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			ILog logger,
			IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			IMovementDataUpdater<MovementBlockData> movementDataUpdater,
			IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable)
			: base(subscriptionService, movementDataMappable, movementDataUpdater, movementGeneratorMappable, logger)
		{

		}
	}
}
