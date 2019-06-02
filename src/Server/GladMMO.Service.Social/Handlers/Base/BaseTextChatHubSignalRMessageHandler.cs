using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public abstract class BaseTextChatHubSignalRMessageHandler<TMessageType> : BaseSignalRMessageHandler<TMessageType, IRemoteSocialTextChatHubClient> 
		where TMessageType : class
	{
		
	}
}
