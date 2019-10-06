using System;
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

	public sealed class ChatChannelJoinedEventArgs : EventArgs
	{
		public ChatChannelType ChannelType { get; }

		public ChatChannelJoinedEventArgs(ChatChannelType channelType)
		{
			if(!Enum.IsDefined(typeof(ChatChannelType), channelType)) throw new InvalidEnumArgumentException(nameof(channelType), (int)channelType, typeof(ChatChannelType));

			ChannelType = channelType;
		}
	}
}
