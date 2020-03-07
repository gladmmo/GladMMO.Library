using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public interface ISocialModelMessageRouter<TRemoteClientHubInterfaceType> : IPeerMessageHandler<BaseSocialModel, BaseSocialModel, HubConnectionMessageContext<TRemoteClientHubInterfaceType>>
	{

	}
}
