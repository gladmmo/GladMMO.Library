﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IChatChannelRoutable
	{
		ChatChannelType ChannelType { get; }
	}
}
