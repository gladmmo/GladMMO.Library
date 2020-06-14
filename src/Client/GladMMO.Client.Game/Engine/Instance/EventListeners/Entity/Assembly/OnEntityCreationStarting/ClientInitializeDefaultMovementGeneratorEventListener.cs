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
			IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			IMovementDataUpdater<MovementBlockData> movementDataUpdater) 
			: base(subscriptionService, movementDataMappable, movementDataUpdater)
		{

		}
	}
}
