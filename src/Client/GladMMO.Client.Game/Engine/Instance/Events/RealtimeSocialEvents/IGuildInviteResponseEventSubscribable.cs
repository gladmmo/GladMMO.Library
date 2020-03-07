using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGuildInviteResponseEventSubscribable
	{
		event EventHandler<GenericSocialEventArgs<GuildMemberInviteResponseModel>> OnGuildMemberInviteResponse;
	}
}
