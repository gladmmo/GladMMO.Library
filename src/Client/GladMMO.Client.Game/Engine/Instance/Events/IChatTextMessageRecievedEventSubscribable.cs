using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public interface IChatTextMessageRecievedEventSubscribable
	{
		event EventHandler<TextChatEventArgs> OnTextChatMessageRecieved;
	}

	/// <summary>
	/// Data and Metadata related to a string chat message.
	/// </summary>
	public sealed class TextChatEventArgs : EventArgs, IChatChannelRoutable
	{
		/// <summary>
		/// The computed full message.
		/// </summary>
		public string Message { get; }

		//The below properties are just for metadata purposes.
		/// <summary>
		/// The sender of the message.
		/// Is empty if there is no sender.
		/// </summary>
		public NetworkEntityGuid Sender { get; }

		/// <summary>
		/// The type of the message.
		/// </summary>
		public ChatChannelType ChannelType { get; }

		/// <inheritdoc />
		public TextChatEventArgs([NotNull] string message, [NotNull] NetworkEntityGuid sender, ChatChannelType messageType)
		{
			if(!Enum.IsDefined(typeof(ChatChannelType), messageType)) throw new InvalidEnumArgumentException(nameof(messageType), (int)messageType, typeof(ChatChannelType));
			Message = message ?? throw new ArgumentNullException(nameof(message));
			Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			ChannelType = messageType;
		}

		/// <inheritdoc />
		public TextChatEventArgs([NotNull] string message, ChatChannelType messageType)
		{
			if(!Enum.IsDefined(typeof(ChatChannelType), messageType)) throw new InvalidEnumArgumentException(nameof(messageType), (int)messageType, typeof(ChatChannelType));
			Message = message ?? throw new ArgumentNullException(nameof(message));
			ChannelType = messageType;
			Sender = NetworkEntityGuid.Empty;
		}
	}
}
