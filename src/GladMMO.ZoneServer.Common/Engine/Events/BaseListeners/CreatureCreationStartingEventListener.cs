using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class CreatureCreationStartingEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		protected CreatureCreationStartingEventListener(IEntityCreationStartingEventSubscribable subscriptionService) : base(subscriptionService)
		{

		}

		protected sealed override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			if(args.EntityGuid.EntityType != EntityType.Creature)
				return;

			OnCreatureEntityCreationStarting(args);
		}

		protected abstract void OnCreatureEntityCreationStarting(EntityCreationStartingEventArgs args);
	}
}
