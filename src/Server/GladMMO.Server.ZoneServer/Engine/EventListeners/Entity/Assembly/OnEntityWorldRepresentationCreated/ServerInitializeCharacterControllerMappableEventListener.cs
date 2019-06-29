using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public class ServerInitializeCharacterControllerMappableEventListener : SharedInitializeCharacterControllerMappableEventListener
	{
		public ServerInitializeCharacterControllerMappableEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService, IEntityGuidMappable<CharacterController> characterControllerMappable) 
			: base(subscriptionService, characterControllerMappable)
		{

		}
	}
}
