using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class OnRealtimeSocialServiceConnectedEventListener : BaseSingleEventListenerInitializable<IRealtimeSocialServiceConnectedEventSubscribable>
	{
		protected OnRealtimeSocialServiceConnectedEventListener(IRealtimeSocialServiceConnectedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
