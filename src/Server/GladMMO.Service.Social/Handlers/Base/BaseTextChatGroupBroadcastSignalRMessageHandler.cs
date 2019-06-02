using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GladMMO
{
	public abstract class BaseTextChatGroupBroadcastSignalRMessageHandler<TMessageType, TSendMessageType> : BaseTextChatHubSignalRMessageHandler<TMessageType>
		where TMessageType : class, ITextMessageContainable, IChatChannelAssociatable
	{
		private ChatChannels TargetChannel { get; }

		private IFactoryCreatable<TSendMessageType, GenericChatMessageContext<TMessageType>> OutgoingMessageFactory { get; }

		/// <inheritdoc />
		protected BaseTextChatGroupBroadcastSignalRMessageHandler(ChatChannels targetChannel, [JetBrains.Annotations.NotNull] IFactoryCreatable<TSendMessageType, GenericChatMessageContext<TMessageType>> outgoingMessageFactory)
		{
			if(!Enum.IsDefined(typeof(ChatChannels), targetChannel)) throw new InvalidEnumArgumentException(nameof(targetChannel), (int)targetChannel, typeof(ChatChannels));

			TargetChannel = targetChannel;
			OutgoingMessageFactory = outgoingMessageFactory ?? throw new ArgumentNullException(nameof(outgoingMessageFactory));
		}

		/// <inheritdoc />
		protected override async Task OnMessageRecieved(IHubConnectionMessageContext<IRemoteSocialTextChatHubClient> context, TMessageType message)
		{
			//TODO: We may want to do validation for the message sent more than this
			if(message.TargetChannel != TargetChannel)
				return;

			//TODO: We need to check if zone messaging is even enabled for the connection. This could be a WEB ONLY connection.
			if(String.IsNullOrWhiteSpace(message.Message))
				return;

			TSendMessageType outgoingMessage = OutgoingMessageFactory.Create(new GenericChatMessageContext<TMessageType>(message, context));

			await SendOutgoingMessage(GetBroadcastGroup(context), outgoingMessage)
				.ConfigureAwait(false);
		}

		protected abstract Task SendOutgoingMessage(IRemoteSocialTextChatHubClient remote, TSendMessageType outgoingMessage);

		/// <summary>
		/// TODO
		/// </summary>
		/// <returns></returns>
		protected abstract IRemoteSocialTextChatHubClient GetBroadcastGroup(IHubConnectionMessageContext<IRemoteSocialTextChatHubClient> context);
	}
}
