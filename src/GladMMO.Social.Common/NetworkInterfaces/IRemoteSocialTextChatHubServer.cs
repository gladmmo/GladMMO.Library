using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for remote interface for Server Hub.
	/// </summary>
	public interface IRemoteSocialTextChatHubServer
	{
		/// <summary>
		/// Async method that sends a <see cref="ZoneChatMessageRequestModel"/>
		/// (a zone text chat message).
		/// </summary>
		/// <param name="message">The message to send.</param>
		/// <returns>Awaitable.</returns>
		Task SendZoneChannelTextChatMessageAsync(ZoneChatMessageRequestModel message);

		/// <summary>
		/// Async method that sends a <see cref="GuildChatMessageRequestModel"/>
		/// (a guild chat message).
		/// </summary>
		/// <param name="message">The message to send.</param>
		/// <returns>Awaitable.</returns>
		Task SendGuildChannelTextChatMessageAsync(GuildChatMessageRequestModel message);
	}
}
