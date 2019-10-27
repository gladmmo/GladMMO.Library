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
	}
}
