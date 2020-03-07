using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.SDK
{
	public interface INetworkGameObjectBehaviour
	{
		GameObjectType BehaviorType { get; }
	}
}
