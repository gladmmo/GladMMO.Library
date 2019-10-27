using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO
{
	public sealed class DefaultRemoteSocialHubClient : IRemoteSocialHubClient, IConnectionHubInitializable
	{
		[CanBeNull]
		public HubConnection Connection { get; set; }

		private ILog Logger { get; }

		public DefaultRemoteSocialHubClient([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task ReceiveGuildInviteResponseAsync([NotNull] GuildMemberInviteResponseModel message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Received Guild Invite Response: {message.ResultCode}");
		}

		public async Task ReceiveGuildInviteEventAsync(GuildMemberInviteEventModel message)
		{
			if(message == null) throw new ArgumentNullException(nameof(message));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Received Guild Invite From: {message.InviterGuid} to GuildId: {message.GuildId}");
		}
	}
}
