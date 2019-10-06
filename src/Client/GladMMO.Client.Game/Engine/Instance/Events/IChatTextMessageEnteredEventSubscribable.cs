using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public interface IChatTextMessageEnteredEventSubscribable
	{
		event EventHandler<ChatTextMessageEnteredEventArgs> OnChatMessageEntered;
	}

	public sealed class ChatTextMessageEnteredEventArgs : EventArgs, IChatChannelRoutable
	{
		/// <summary>
		/// The requested channel type of the message.
		/// </summary>
		public ChatChannelType ChannelType { get; }

		public string Content { get; }

		public ChatTextMessageEnteredEventArgs(ChatChannelType requestedChannelType, [NotNull] string content)
		{
			if (!Enum.IsDefined(typeof(ChatChannelType), requestedChannelType)) throw new InvalidEnumArgumentException(nameof(requestedChannelType), (int) requestedChannelType, typeof(ChatChannelType));
			if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

			ChannelType = requestedChannelType;
			Content = content;
		}
	}
}
