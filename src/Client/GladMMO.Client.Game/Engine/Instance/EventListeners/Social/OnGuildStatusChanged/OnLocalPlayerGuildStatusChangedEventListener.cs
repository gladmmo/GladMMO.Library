using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class OnLocalPlayerGuildStatusChangedEventListener : BaseSingleEventListenerInitializable<IGuildStatusChangedEventSubscribable, GenericSocialEventArgs<GuildStatusChangedEventModel>>
	{
		protected IReadonlyLocalPlayerDetails LocalPlayerDetails { get; }

		protected OnLocalPlayerGuildStatusChangedEventListener(IGuildStatusChangedEventSubscribable subscriptionService,
			[NotNull] IReadonlyLocalPlayerDetails localPlayerDetails) 
			: base(subscriptionService)
		{
			LocalPlayerDetails = localPlayerDetails ?? throw new ArgumentNullException(nameof(localPlayerDetails));
		}

		protected sealed override void OnEventFired(object source, GenericSocialEventArgs<GuildStatusChangedEventModel> args)
		{
			//Only if it's matching the local player should we dispatch.
			if(LocalPlayerDetails.LocalPlayerGuid == args.Data.EntityGuid)
				OnGuildStatusChanged(args.Data);
		}

		protected abstract void OnGuildStatusChanged(GuildStatusChangedEventModel changeArgs);
	}
}
