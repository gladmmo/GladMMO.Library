using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientDestroyWorldRepresentationEventListener : SharedDestroyWorldRepresentationEventListener
	{
		public ClientDestroyWorldRepresentationEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService, IReadonlyEntityGuidMappable<GameObject> guidToGameObjectMappable, IGameObjectToEntityMappable gameObjectToEntityMap) 
			: base(subscriptionService, guidToGameObjectMappable, gameObjectToEntityMap)
		{

		}
	}
}
