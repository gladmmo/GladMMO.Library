using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ServerDestroyWorldRepresentationEventListener : SharedDestroyWorldRepresentationEventListener
	{
		public ServerDestroyWorldRepresentationEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService, IReadonlyEntityGuidMappable<GameObject> guidToGameObjectMappable, IGameObjectToEntityMappable gameObjectToEntityMap) 
			: base(subscriptionService, guidToGameObjectMappable, gameObjectToEntityMap)
		{

		}
	}
}
