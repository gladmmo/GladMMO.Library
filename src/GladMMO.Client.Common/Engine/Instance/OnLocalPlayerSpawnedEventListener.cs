using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class OnLocalPlayerSpawnedEventListener : BaseSingleEventListenerInitializable<ILocalPlayerSpawnedEventSubscribable, LocalPlayerSpawnedEventArgs>
	{
		/// <inheritdoc />
		protected OnLocalPlayerSpawnedEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, LocalPlayerSpawnedEventArgs args)
		{
			OnLocalPlayerSpawned(args);
		}

		protected abstract void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args);
	}
}
