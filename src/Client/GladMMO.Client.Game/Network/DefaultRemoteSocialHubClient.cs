using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO
{
	public sealed class DefaultRemoteSocialHubClient : IRemoteSocialHubClient, IConnectionHubInitializable,
		IGuildInviteResponseEventSubscribable, IGuildMemberInviteEventEventSubscribable
	{
		[CanBeNull]
		public HubConnection Connection { get; set; }

		private ILog Logger { get; }

		//not normal event from this client because other things publish this event
		//within the client.
		private ICharacterJoinedGuildEventPublisher JoinedGuildEventPublisher { get; }

		//The event publishers for the received data.
		public event EventHandler<GenericSocialEventArgs<GuildMemberInviteResponseModel>> OnGuildMemberInviteResponse;

		public event EventHandler<GenericSocialEventArgs<GuildMemberInviteEventModel>> OnGuildMemberInviteEvent;

		public event EventHandler<GenericSocialEventArgs<GuildMemberJoinedEventModel>> OnGuildMemberJoined;

		public DefaultRemoteSocialHubClient([NotNull] ILog logger,
			[NotNull] ICharacterJoinedGuildEventPublisher joinedGuildEventPublisher)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			JoinedGuildEventPublisher = joinedGuildEventPublisher ?? throw new ArgumentNullException(nameof(joinedGuildEventPublisher));
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
			//Not hidden, we can show people this is raised event.
			JoinedGuildEventPublisher.PublishEvent(this, new CharacterJoinedGuildEventArgs(message.JoineeGuid, false));
		}
	}
}
