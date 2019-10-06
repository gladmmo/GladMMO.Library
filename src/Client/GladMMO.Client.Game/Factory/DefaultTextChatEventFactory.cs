using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultTextChatEventFactory : ITextChatEventFactory
	{
		/// <inheritdoc />
		public TextChatEventArgs CreateChatData<TMessageType>([NotNull] EntityAssociatedData<TMessageType> incomingChatMessageEventData, [NotNull] string associatedEntityName)
			where TMessageType : ITextMessageContainable, IChatChannelAssociatable
		{
			if(incomingChatMessageEventData == null) throw new ArgumentNullException(nameof(incomingChatMessageEventData));
			if(associatedEntityName == null) throw new ArgumentNullException(nameof(associatedEntityName));

			/*ChatMessageType messageType = MessageTypeFromChannel(incomingChatMessageEventData.Data.TargetChannel);

			string renderableMessage = $"<color=#{ComputeColorFromChatType(messageType)}>{ComputeChannelText(messageType)} {associatedEntityName}: {incomingChatMessageEventData.Data.Message}</color>";

			return new TextChatEventData(renderableMessage, incomingChatMessageEventData.EntityGuid, messageType);*/
			throw new NotImplementedException($"Haven't reimplemented chat yet.");
		}

		/// <inheritdoc />
		public TextChatEventArgs CreateChatData<TMessageType>([NotNull] TMessageType incomingChatMessageEventData) 
			where TMessageType : ITextMessageContainable, IChatChannelAssociatable
		{
			if(incomingChatMessageEventData == null) throw new ArgumentNullException(nameof(incomingChatMessageEventData));

			/*ChatMessageType messageType = MessageTypeFromChannel(incomingChatMessageEventData.TargetChannel);

			string renderableMessage = $"<color=#{ComputeColorFromChatType(messageType)}>{ComputeChannelText(messageType)}: {incomingChatMessageEventData.Message}</color>";

			return new TextChatEventData(renderableMessage, messageType);*/
			throw new NotImplementedException($"Haven't reimplemented chat yet.");
		}

		private string ComputeColorFromChatType(ChatChannelType messageType)
		{
			switch(messageType)
			{
				case ChatChannelType.System:
					return "ff0000ff";
				case ChatChannelType.Zone:
					return "AA9E92ff";
				case ChatChannelType.Guild:
					return "42f442ff";
			}

			throw new NotImplementedException($"Cannot handle Chat Type: {messageType}:{(int)messageType}");
		}

		private string ComputeChannelText(ChatChannelType messageType)
		{
			switch(messageType)
			{
				case ChatChannelType.System:
					return "[System]";
				case ChatChannelType.Zone:
					return "[1. Zone]";
				case ChatChannelType.Guild:
					return "[Guild]";
			}

			throw new NotImplementedException($"Cannot handle Chat Type: {messageType}:{(int)messageType}");
		}

		private ChatChannelType MessageTypeFromChannel(ChatChannels channel)
		{
			switch(channel)
			{
				case ChatChannels.Internal:
					return ChatChannelType.System;
				case ChatChannels.ZoneChannel:
					return ChatChannelType.Zone;
				case ChatChannels.GuildChannel:
					return ChatChannelType.Guild;
			}

			throw new NotImplementedException($"Cannot handle Chat Channel: {channel}:{(int)channel}");
		}
	}
}