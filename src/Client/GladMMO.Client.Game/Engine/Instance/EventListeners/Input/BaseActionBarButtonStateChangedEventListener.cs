using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseActionBarButtonStateChangedEventListener : BaseSingleEventListenerInitializable<IActionBarButtonStateChangedEventSubscribable, ActionBarButtonStateChangedEventArgs>
	{
		protected BaseActionBarButtonStateChangedEventListener(IActionBarButtonStateChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
