using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public sealed class GuildMessageBroadcastMessageHandler : BaseTextChatGroupBroadcastSignalRMessageHandler<GuildChatMessageRequestModel, GuildChatMessageEventModel>
	{
		//TODO: Don't directly store response, store guild status model.
		private IEntityGuidMappable<CharacterGuildMembershipStatusResponse> GuildStatusMappable { get; }

		/// <inheritdoc />
		public GuildMessageBroadcastMessageHandler(
			[JetBrains.Annotations.NotNull] IFactoryCreatable<GuildChatMessageEventModel, GenericChatMessageContext<GuildChatMessageRequestModel>> outgoingMessageFactory,
			[JetBrains.Annotations.NotNull] IEntityGuidMappable<CharacterGuildMembershipStatusResponse> guildStatusMappable) 
			: base(ChatChannels.GuildChannel, outgoingMessageFactory)
		{
			GuildStatusMappable = guildStatusMappable ?? throw new ArgumentNullException(nameof(guildStatusMappable));
		}

		/// <inheritdoc />
		protected override Task SendOutgoingMessage(IRemoteSocialTextChatHubClient remote, GuildChatMessageEventModel outgoingMessage)
		{
			return remote.RecieveGuildChannelTextChatMessageAsync(outgoingMessage);
		}

		/// <inheritdoc />
		protected override IRemoteSocialTextChatHubClient GetBroadcastGroup(IHubConnectionMessageContext<IRemoteSocialTextChatHubClient> context)
		{
			if(!GuildStatusMappable.ContainsKey(context.CallerGuid))
				throw new InvalidOperationException($"Tried to send Guild Message for Entity: {context.CallerGuid} but no guild data was available.");

			//TODO: We should have guild status model, including pending invites and such.
			if(!GuildStatusMappable[context.CallerGuid].isSuccessful)
			{
				//TODO: Log
			}
			
			return context.Clients.Group($"guild:{GuildStatusMappable[context.CallerGuid].GuildId}");
		}
	}
}
