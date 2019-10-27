using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGuildStatusChangedEventSubscribable
	{
		event EventHandler<GenericSocialEventArgs<GuildStatusChangedEventModel>> OnGuildStatusChanged;
	}
}
