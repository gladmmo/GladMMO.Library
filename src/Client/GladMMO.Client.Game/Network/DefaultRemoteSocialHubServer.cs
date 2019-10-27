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

		public async Task SendTestMessageAsync(TestSocialModel testModel)
		{
			if(CheckConnectionInitialized())
				throw new NotImplementedException();
		}

		public async Task SendGuildInviteRequestAsync(GuildMemberInviteRequestModel invitationRequest)
		{
			if(CheckConnectionInitialized())
				throw new NotImplementedException();
		}

		private bool CheckConnectionInitialized()
		{
			if (Connection == null)
				if(Logger.IsErrorEnabled)
					Logger.Error($"Social connection not initialized.");

			return Connection != null;
		}
	}
}
