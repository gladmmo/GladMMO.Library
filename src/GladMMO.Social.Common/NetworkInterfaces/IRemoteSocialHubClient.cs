using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IRemoteSocialHubClient
	{
		/*/// <summary>
		/// Async method that handles recieveing a <see cref="ZoneChatMessageEventModel"/>
		/// (a zone text chat message) with information about the sender.
		/// </summary>
		/// <param name="message">The message to send.</param>
		/// <returns>Awaitable.</returns>
		Task RecieveZoneChannelTextChatMessageAsync(ZoneChatMessageEventModel message);

		/// <summary>
		/// Async method that handles recieveing a <see cref="GuildChatMessageEventModel"/>
		/// (a guild text chat message) with information about the sender.
		/// </summary>
		/// <param name="message">The message to send.</param>
		/// <returns>Awaitable.</returns>
		Task RecieveGuildChannelTextChatMessageAsync(GuildChatMessageEventModel message);*/

		Task ReceiveGuildInviteResponseAsync(GuildMemberInviteResponseModel message);

		Task ReceiveGuildInviteEventAsync(GuildMemberInviteEventModel message);

		Task ReceiveGuildMemberJoinedEventAsync(GuildMemberJoinedEventModel message);

		Task ReceiveGuildStatusChangedEventAsync(GuildStatusChangedEventModel message);
	}
}
