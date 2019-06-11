using System;
using System.Collections.Generic;
using System.Text;
using VivoxUnity;

namespace GladMMO
{
	public interface IReadonlyPositionalVoiceChannels : IEnumerable<IChannelSession>
	{

	}

	public interface IPositionalVoiceChannels : IReadonlyPositionalVoiceChannels, ICollection<IChannelSession>
	{

	}
}
