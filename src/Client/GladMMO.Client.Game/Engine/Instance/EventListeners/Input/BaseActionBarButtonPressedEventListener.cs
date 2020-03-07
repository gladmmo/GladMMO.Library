using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseActionBarButtonPressedEventListener : BaseSingleEventListenerInitializable<IActionBarButtonPressedEventSubscribable, ActionBarButtonPressedEventArgs>
	{
		protected BaseActionBarButtonPressedEventListener(IActionBarButtonPressedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected override void OnEventFired(object source, [NotNull] ActionBarButtonPressedEventArgs args)
		{
			if (args == null) throw new ArgumentNullException(nameof(args));

			OnActionBarButtonPressed(args.Index);
		}

		protected abstract void OnActionBarButtonPressed(ActionBarIndex index);
	}
}
