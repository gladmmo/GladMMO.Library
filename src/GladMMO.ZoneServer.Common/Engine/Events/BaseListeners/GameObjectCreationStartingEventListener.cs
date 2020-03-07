﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class GameObjectCreationStartingEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		protected GameObjectCreationStartingEventListener(IEntityCreationStartingEventSubscribable subscriptionService) : base(subscriptionService)
		{

		}

		protected sealed override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			if(args.EntityGuid.TypeId != EntityTypeId.TYPEID_GAMEOBJECT)
				return;

			OnGameObjectEntityCreationStarting(args);
		}

		protected abstract void OnGameObjectEntityCreationStarting(EntityCreationStartingEventArgs args);
	}
}
