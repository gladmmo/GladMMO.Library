using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ITextChatEventFactory
	{
		TextChatEventArgs CreateChatData<TMessageType>(EntityAssociatedData<TMessageType> incomingChatMessageEventData, string associatedEntityName)
			where TMessageType : ITextMessageContainable, IChatChannelAssociatable;

		TextChatEventArgs CreateChatData<TMessageType>(TMessageType incomingChatMessageEventData)
			where TMessageType : ITextMessageContainable, IChatChannelAssociatable;
	}
}
