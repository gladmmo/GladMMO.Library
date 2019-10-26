using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class OnLocalPlayerGuildStatusChangedEventListener : BaseSingleEventListenerInitializable<IGuildStatusChangedEventSubscribable, GuildStatusChangedEventArgs>
	{
		protected IReadonlyLocalPlayerDetails LocalPlayerDetails { get; }

		protected OnLocalPlayerGuildStatusChangedEventListener(IGuildStatusChangedEventSubscribable subscriptionService,
			[NotNull] IReadonlyLocalPlayerDetails localPlayerDetails) 
			: base(subscriptionService)
		{
			LocalPlayerDetails = localPlayerDetails ?? throw new ArgumentNullException(nameof(localPlayerDetails));
		}

		protected sealed override void OnEventFired(object source, GuildStatusChangedEventArgs args)
		{
			//Only if it's matching the local player should we dispatch.
			if(LocalPlayerDetails.LocalPlayerGuid == args.EntityGuid)
				OnGuildStatusChanged(args);
		}

		protected abstract void OnGuildStatusChanged(GuildStatusChangedEventArgs changeArgs);
	}
}
