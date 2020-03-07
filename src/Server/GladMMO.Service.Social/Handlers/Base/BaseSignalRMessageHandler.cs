using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public abstract class BaseSignalRMessageHandler<TMessageType, TRemoteClientHubInterfaceType> : IPeerPayloadSpecificMessageHandler<TMessageType, object, HubConnectionMessageContext<TRemoteClientHubInterfaceType>>, ISocialModelPayloadHandler<TRemoteClientHubInterfaceType>
		where TMessageType : BaseSocialModel
	{
		public Type PayloadType => typeof(TMessageType);

		public Task HandleMessage(HubConnectionMessageContext<TRemoteClientHubInterfaceType> context, TMessageType payload)
		{
			//We just forward the message to handling and expose ONLY the hub connection message context interface
			//so that nobody calls the GladNet3 stuff.
			return OnMessageRecieved(context, payload);
		}

		protected abstract Task OnMessageRecieved(IHubConnectionMessageContext<TRemoteClientHubInterfaceType> context, TMessageType payload);

		public bool CanHandle([JetBrains.Annotations.NotNull] NetworkIncomingMessage<BaseSocialModel> message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			return message.Payload is TMessageType;
		}

		public async Task<bool> TryHandleMessage([JetBrains.Annotations.NotNull] HubConnectionMessageContext<TRemoteClientHubInterfaceType> context, [JetBrains.Annotations.NotNull] NetworkIncomingMessage<BaseSocialModel> message)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (message == null) throw new ArgumentNullException(nameof(message));

			if (CanHandle(message))
			{
				await HandleMessage(context, (TMessageType) message.Payload);
				return true;
			}

			return false;
		}
	}
}
