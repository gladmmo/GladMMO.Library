using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.Chat
{
	public interface IChatChannelRoutable
	{
		ChatChannelType ChannelType { get; }
	}
}
