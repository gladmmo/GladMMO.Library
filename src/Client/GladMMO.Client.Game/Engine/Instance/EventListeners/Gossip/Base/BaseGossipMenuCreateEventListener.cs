using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseGossipMenuCreateEventListener : BaseSingleEventListenerInitializable<IGossipMenuCreateEventSubscribable, GossipMenuCreateEventArgs>
	{
		protected BaseGossipMenuCreateEventListener(IGossipMenuCreateEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
