using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public abstract class BaseSignalRMessageHandler<TMessageType, TRemoteClientHubInterfaceType> : IPeerPayloadSpecificMessageHandler<TMessageType, object, HubConnectionMessageContext<TRemoteClientHubInterfaceType>> 
		where TMessageType : class
	{
		public Task HandleMessage(HubConnectionMessageContext<TRemoteClientHubInterfaceType> context, TMessageType payload)
		{
			//We just forward the message to handling and expose ONLY the hub connection message context interface
			//so that nobody calls the GladNet3 stuff.
			return OnMessageRecieved(context, payload);
		}

		protected abstract Task OnMessageRecieved(IHubConnectionMessageContext<TRemoteClientHubInterfaceType> context, TMessageType payload);
	}
}
