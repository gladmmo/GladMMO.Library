using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseOnServerStartedEventListener : BaseSingleEventListenerInitializable<IServerStartedEventSubscribable>
	{
		protected BaseOnServerStartedEventListener(IServerStartedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
