using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class EntityCreationFinishedEventListener : BaseSingleEventListenerInitializable<IEntityCreationFinishedEventSubscribable, EntityCreationFinishedEventArgs>
	{
		protected EntityCreationFinishedEventListener(IEntityCreationFinishedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
