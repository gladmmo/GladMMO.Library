using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public sealed class ZoneMessageBroadcastMessageHandler : BaseTextChatGroupBroadcastSignalRMessageHandler<ZoneChatMessageRequestModel, ZoneChatMessageEventModel>
	{
		//I am not happy about this, but we need to maintain some state so that we know what zone a connection is in.
		private IConnectionToZoneMappable ZoneLookupService { get; }

		/// <inheritdoc />
		public ZoneMessageBroadcastMessageHandler([JetBrains.Annotations.NotNull] IFactoryCreatable<ZoneChatMessageEventModel, GenericChatMessageContext<ZoneChatMessageRequestModel>> outgoingMessageFactory, [JetBrains.Annotations.NotNull] IConnectionToZoneMappable zoneLookupService) 
			: base(ChatChannels.ZoneChannel, outgoingMessageFactory)
		{
			ZoneLookupService = zoneLookupService ?? throw new ArgumentNullException(nameof(zoneLookupService));
		}

		/// <inheritdoc />
		protected override async Task SendOutgoingMessage(IRemoteSocialTextChatHubClient remote, ZoneChatMessageEventModel outgoingMessage)
		{
			await remote.RecieveZoneChannelTextChatMessageAsync(outgoingMessage)
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		protected override IRemoteSocialTextChatHubClient GetBroadcastGroup(IHubConnectionMessageContext<IRemoteSocialTextChatHubClient> context)
		{
			return context.Clients.Group($"zone:{ZoneLookupService.Retrieve(context.HubConntext.ConnectionId)}");
		}
	}
}
