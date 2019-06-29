using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Simplified base-type for single event listeners that listen to: <see cref="IEntityWorldRepresentationCreatedEventSubscribable"/>.
	/// </summary>
	public abstract class EntityWorldRepresentationCreatedEventListener : BaseSingleEventListenerInitializable<IEntityWorldRepresentationCreatedEventSubscribable, EntityWorldRepresentationCreatedEventArgs>
	{
		protected EntityWorldRepresentationCreatedEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected sealed override void OnEventFired(object source, EntityWorldRepresentationCreatedEventArgs args)
		{
			OnEntityWorldRepresentationCreated(args);
		}

		protected abstract void OnEntityWorldRepresentationCreated(EntityWorldRepresentationCreatedEventArgs args);
	}
}
