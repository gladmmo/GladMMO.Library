using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using VivoxUnity;

namespace GladMMO
{
	public interface IReadonlyPositionalVoiceChannelCollection : IEnumerable<IChannelSession>
	{

	}

	public interface IPositionalVoiceChannelCollection : IReadonlyPositionalVoiceChannelCollection, ICollection<IChannelSession>
	{

	}
}
