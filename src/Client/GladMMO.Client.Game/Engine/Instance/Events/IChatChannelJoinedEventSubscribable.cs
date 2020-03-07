using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VivoxUnity;

namespace GladMMO
{
	public interface IChatChannelJoinedEventSubscribable
	{
		event EventHandler<ChatChannelJoinedEventArgs> OnChatChannelJoined;
	}

	public sealed class ChatChannelJoinedEventArgs : EventArgs, IChatChannelRoutable
	{
		public ChatChannelType ChannelType { get; }

		public IVivoxTextChannelMessageSubscribable Channel { get; }

		public IChatChannelSender MessageSender { get; }

		public ChatChannelJoinedEventArgs(ChatChannelType channelType, [NotNull] IVivoxTextChannelMessageSubscribable channel, [NotNull] IChatChannelSender messageSender)
		{
			if(!Enum.IsDefined(typeof(ChatChannelType), channelType)) throw new InvalidEnumArgumentException(nameof(channelType), (int)channelType, typeof(ChatChannelType));

			ChannelType = channelType;
			Channel = channel ?? throw new ArgumentNullException(nameof(channel));
			MessageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
		}
	}
}
