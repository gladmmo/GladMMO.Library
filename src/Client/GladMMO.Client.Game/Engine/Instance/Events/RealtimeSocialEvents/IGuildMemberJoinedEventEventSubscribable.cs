using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGuildMemberJoinedEventEventSubscribable
	{
		event EventHandler<GenericSocialEventArgs<GuildMemberJoinedEventModel>> OnGuildMemberJoined;
	}
}
