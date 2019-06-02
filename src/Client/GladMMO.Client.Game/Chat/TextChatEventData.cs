using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Data and Metadata related to a string chat message.
	/// </summary>
	public sealed class TextChatEventData
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
		public ChatMessageType MessageType { get; }

		/// <inheritdoc />
		public TextChatEventData([NotNull] string message, [NotNull] NetworkEntityGuid sender, ChatMessageType messageType)
		{
			if(!Enum.IsDefined(typeof(ChatMessageType), messageType)) throw new InvalidEnumArgumentException(nameof(messageType), (int)messageType, typeof(ChatMessageType));
			Message = message ?? throw new ArgumentNullException(nameof(message));
			Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			MessageType = messageType;
		}

		/// <inheritdoc />
		public TextChatEventData([NotNull] string message, ChatMessageType messageType)
		{
			if(!Enum.IsDefined(typeof(ChatMessageType), messageType)) throw new InvalidEnumArgumentException(nameof(messageType), (int)messageType, typeof(ChatMessageType));
			Message = message ?? throw new ArgumentNullException(nameof(message));
			MessageType = messageType;
			Sender = NetworkEntityGuid.Empty;
		}
	}
}
