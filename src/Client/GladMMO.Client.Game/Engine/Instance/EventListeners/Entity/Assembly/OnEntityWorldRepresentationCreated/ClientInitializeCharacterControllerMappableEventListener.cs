using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class ClientInitializeCharacterControllerMappableEventListener : SharedInitializeCharacterControllerMappableEventListener
	{
		public ClientInitializeCharacterControllerMappableEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService, IEntityGuidMappable<CharacterController> characterControllerMappable) 
			: base(subscriptionService, characterControllerMappable)
		{

		}
	}
}
