using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class OnLocalPlayerGuildStatusChangedEventListener : BaseSingleEventListenerInitializable<IGuildStatusChangedEventSubscribable, GuildStatusChangedEventArgs>
	{
		protected OnLocalPlayerGuildStatusChangedEventListener(IGuildStatusChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
