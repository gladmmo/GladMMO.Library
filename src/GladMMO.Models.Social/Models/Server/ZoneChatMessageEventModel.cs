using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Data model for <see cref="TargetlessChannelChatMessageEventModel"/>
	/// for the <see cref="ChatChannels"/>.ZoneChannel. Contains
	/// a wrapped <see cref="NetworkEntityGuid"/> associated zone chat message.
	/// </summary>
	[JsonObject]
	public sealed class ZoneChatMessageEventModel : TargetlessChannelChatMessageEventModel
	{
		public ZoneChatMessageEventModel([JetBrains.Annotations.NotNull] EntityAssociatedData<TargetlessChannelChatMessageRequestModel> channelMessage) 
			: base(channelMessage)
		{
			
		}

		private ZoneChatMessageEventModel()
			: base()
		{
			
		}
	}
}
