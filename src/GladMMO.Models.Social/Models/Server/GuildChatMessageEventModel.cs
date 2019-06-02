using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Data model for <see cref="TargetlessChannelChatMessageEventModel"/>
	/// for the <see cref="ChatChannels"/>.GuildChannel. Contains
	/// a wrapped <see cref="NetworkEntityGuid"/> associated guild chat message.
	/// </summary>
	[JsonObject]
	public sealed class GuildChatMessageEventModel : TargetlessChannelChatMessageEventModel
	{
		public GuildChatMessageEventModel([JetBrains.Annotations.NotNull] EntityAssociatedData<TargetlessChannelChatMessageRequestModel> channelMessage)
			: base(channelMessage)
		{

		}

		private GuildChatMessageEventModel()
			: base()
		{

		}
	}
}