using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet;

namespace GladMMO
{
	//IPeerPayloadSpecificMessageHandler<TMessageType, object, HubConnectionMessageContext<TRemoteClientHubInterfaceType>>
	public interface ISocialModelPayloadHandler<TRemoteClientHubInterfaceType> : IPeerMessageHandler<BaseSocialModel, BaseSocialModel, HubConnectionMessageContext<TRemoteClientHubInterfaceType>>
	{
		Type PayloadType { get; }
	}
}