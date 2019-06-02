using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Data model for <see cref="TargetlessChannelChatMessageRequestModel"/>
	/// for the <see cref="ChatChannels"/>.GuildChannel.
	/// </summary>
	[JsonObject]
	public sealed class GuildChatMessageRequestModel : TargetlessChannelChatMessageRequestModel
	{
		/// <inheritdoc />
		public GuildChatMessageRequestModel(string message)
			: base(message, ChatChannels.GuildChannel)
		{

		}

		private GuildChatMessageRequestModel()
			: base()
		{

		}
	}
}
