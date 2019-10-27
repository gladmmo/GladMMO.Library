using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for remote interface for Server Hub.
	/// </summary>
	public interface IRemoteSocialHubServer
	{
		Task SendTestMessageAsync(TestSocialModel testModel);

		Task SendGuildInviteRequestAsync(GuildMemberInviteRequestModel invitationRequest);

		Task SendGuildInviteEventResponseAsync(PendingGuildInviteHandleRequest message);
	}
}
