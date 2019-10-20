using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO
{
	public sealed class SignalRForwardedIRemoteSocialTextChatHubClient : IRemoteSocialTextChatHubServer
	{
		private HubConnection Connection { get; }

		/// <inheritdoc />
		public SignalRForwardedIRemoteSocialTextChatHubClient([JetBrains.Annotations.NotNull] HubConnection connection)
		{
			Connection = connection ?? throw new ArgumentNullException(nameof(connection));
		}

		/// <inheritdoc />
		/*public Task SendZoneChannelTextChatMessageAsync(ZoneChatMessageRequestModel message)
		{
			return Connection.SendAsync(nameof(SendZoneChannelTextChatMessageAsync), message);
		}

		/// <inheritdoc />
		public Task SendGuildChannelTextChatMessageAsync(GuildChatMessageRequestModel message)
		{
			return Connection.SendAsync(nameof(SendGuildChannelTextChatMessageAsync), message);
		}*/
	}
}
