using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class PlayerCreationStartingEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		protected PlayerCreationStartingEventListener(IEntityCreationStartingEventSubscribable subscriptionService) : base(subscriptionService)
		{

		}

		protected sealed override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			if(args.EntityGuid.EntityType != EntityType.Player)
				return;

			OnPlayerEntityCreationStarting(args);
		}

		protected abstract void OnPlayerEntityCreationStarting(EntityCreationStartingEventArgs args);
	}
}
