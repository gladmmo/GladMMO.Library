using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Base event listener type for <see cref="EntityCreationFinishedEventListener"/> who are
	/// only listening for Player entity creation finishing.
	/// </summary>
	public abstract class PlayerCreationFinishedEventListener : EntityCreationFinishedEventListener
	{
		protected PlayerCreationFinishedEventListener(IEntityCreationFinishedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected sealed override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			OnEntityCreationFinished(args);
		}
	}
}
