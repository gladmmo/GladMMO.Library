using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class EntityCreationStartingEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		protected EntityCreationStartingEventListener(IEntityCreationStartingEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
