using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class EntityAvatarChangedEventListener : BaseSingleEventListenerInitializable<IEntityAvatarChangedEventSubscribable, EntityAvatarChangedEventArgs>
	{
		protected EntityAvatarChangedEventListener(IEntityAvatarChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
