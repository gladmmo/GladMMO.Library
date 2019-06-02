using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Data model for a message <see cref="Message"/> to be sent to
	/// a target channel <see cref="TargetChannel"/>
	/// </summary>
	[JsonObject]
	public class TargetlessChannelChatMessageRequestModel : ITextMessageContainable, IChatChannelAssociatable
	{
		/// <summary>
		/// The channel to attempt to send the <see cref="Message"/> on.
		/// </summary>
		[JsonProperty]
		public ChatChannels TargetChannel { get; protected set; } //has to be protected so reflection CanWrite is true for child types

		/// <summary>
		/// The chat message intended to be sent.
		/// </summary>
		[JsonProperty]
		public string Message { get; protected set; } //has to be protected so reflection CanWrite is true for child types

		/// <inheritdoc />
		public TargetlessChannelChatMessageRequestModel(string message, ChatChannels targetChannel)
		{
			if(string.IsNullOrEmpty(message)) throw new ArgumentException("Value cannot be null or empty.", nameof(message));
			if(!Enum.IsDefined(typeof(ChatChannels), targetChannel)) throw new InvalidEnumArgumentException(nameof(targetChannel), (int)targetChannel, typeof(ChatChannels));

			Message = message;
			TargetChannel = targetChannel;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected TargetlessChannelChatMessageRequestModel()
		{
			
		}
	}
}
