using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Simplified base-type for single event listeners that listen to: <see cref="IEntityWorldRepresentationCreatedEventSubscribable"/>.
	/// For players only though.
	/// </summary>
	public abstract class PlayerWorldRepresentationCreatedEventListener : EntityWorldRepresentationCreatedEventListener
	{
		protected PlayerWorldRepresentationCreatedEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected sealed override void OnEntityWorldRepresentationCreated(EntityWorldRepresentationCreatedEventArgs args)
		{
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			OnPlayerWorldRepresentationCreated(args);
		}

		protected abstract void OnPlayerWorldRepresentationCreated(EntityWorldRepresentationCreatedEventArgs args);
	}
}
