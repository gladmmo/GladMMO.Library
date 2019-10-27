using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO
{
	public sealed class DefaultRemoteSocialHubServer : IRemoteSocialHubServer, IConnectionHubInitializable
	{
		[CanBeNull]
		public HubConnection Connection { get; set; }

		private ILog Logger { get; }

		public DefaultRemoteSocialHubServer([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public Task SendTestMessageAsync(TestSocialModel model)
		{
			if(CheckConnectionInitialized())
				return Connection.SendAsync(nameof(SendTestMessageAsync), model);

			return Task.CompletedTask;
		}

		public Task SendGuildInviteRequestAsync(GuildMemberInviteRequestModel model)
		{
			if(CheckConnectionInitialized())
				return Connection.SendAsync(nameof(SendGuildInviteRequestAsync), model);

			return Task.CompletedTask;
		}

		public Task SendGuildInviteEventResponseAsync(PendingGuildInviteHandleRequest message)
		{
			if(CheckConnectionInitialized())
				return Connection.SendAsync(nameof(SendGuildInviteEventResponseAsync), message);

			return Task.CompletedTask;
		}

		private bool CheckConnectionInitialized()
		{
			if (Connection == null || Connection.State == HubConnectionState.Disconnected)
				if(Logger.IsErrorEnabled)
					Logger.Error($"Social connection not initialized.");

			return Connection != null;
		}
	}
}
