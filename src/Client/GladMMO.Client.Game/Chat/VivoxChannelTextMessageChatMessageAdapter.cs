using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VivoxUnity;

namespace GladMMO.Chat
{
	public sealed class VivoxChannelTextMessageChatMessageAdapter : IChatChannelRoutable, ITextMessageContainable
	{
		private IChannelTextMessage ChannelMessage { get; }

		public ChatChannelType ChannelType { get; }

		public string Message => ChannelMessage.Message;

		public VivoxChannelTextMessageChatMessageAdapter([NotNull] IChannelTextMessage channelMessage, ChatChannelType channelType)
		{
			if (!Enum.IsDefined(typeof(ChatChannelType), channelType)) throw new InvalidEnumArgumentException(nameof(channelType), (int) channelType, typeof(ChatChannelType));

			ChannelMessage = channelMessage ?? throw new ArgumentNullException(nameof(channelMessage));
			ChannelType = channelType;
		}
	}
}
