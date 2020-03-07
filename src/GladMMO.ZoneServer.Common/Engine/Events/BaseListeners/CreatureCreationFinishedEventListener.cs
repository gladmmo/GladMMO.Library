using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Base event listener type for <see cref="EntityCreationFinishedEventListener"/> who are
	/// only listening for Creature entity creation finishing.
	/// </summary>
	public abstract class CreatureCreationFinishedEventListener : EntityCreationFinishedEventListener
	{
		protected CreatureCreationFinishedEventListener(IEntityCreationFinishedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected sealed override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			if (args.EntityGuid.TypeId != EntityTypeId.TYPEID_UNIT)
				return;

			OnEntityCreationFinished(args);
		}
	}
}
