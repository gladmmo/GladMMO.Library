using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO
{
	public sealed class DefaultRemoteSocialHubClient : IRemoteSocialHubClient, IConnectionHubInitializable,
		IGuildInviteResponseEventSubscribable, IGuildMemberInviteEventEventSubscribable, IGuildMemberJoinedEventEventSubscribable
	{
		[CanBeNull]
		public HubConnection Connection { get; set; }

		private ILog Logger { get; }

		//The event publishers for the received data.
		public event EventHandler<GenericSocialEventArgs<GuildMemberInviteResponseModel>> OnGuildMemberInviteResponse;

		public event EventHandler<GenericSocialEventArgs<GuildMemberInviteEventModel>> OnGuildMemberInviteEvent;

		public event EventHandler<GenericSocialEventArgs<GuildMemberJoinedEventModel>> OnGuildMemberJoined;

		public DefaultRemoteSocialHubClient([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task ReceiveGuildInviteResponseAsync([NotNull] GuildMemberInviteResponseModel message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			OnGuildMemberInviteResponse?.Invoke(this, GenericSocialEventArgs.Create(message));
		}

		public async Task ReceiveGuildInviteEventAsync(GuildMemberInviteEventModel message)
		{
			if(message == null) throw new ArgumentNullException(nameof(message));

			OnGuildMemberInviteEvent?.Invoke(this, GenericSocialEventArgs.Create(message));
		}

		public async Task ReceiveGuildMemberJoinedEventAsync(GuildMemberJoinedEventModel message)
		{
			OnGuildMemberJoined?.Invoke(this, GenericSocialEventArgs.Create(message));
		}
	}
}
