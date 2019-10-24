using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public sealed class SocialSignalRMessageRouter<TRemoteClientHubInterfaceType> : ISocialModelMessageRouter<TRemoteClientHubInterfaceType>
	{
		private Dictionary<Type, ISocialModelPayloadHandler<TRemoteClientHubInterfaceType>> MessageRouteMap { get; }

		public SocialSignalRMessageRouter([JetBrains.Annotations.NotNull] IEnumerable<ISocialModelPayloadHandler<TRemoteClientHubInterfaceType>> messageHandlers)
		{
			if (messageHandlers == null) throw new ArgumentNullException(nameof(messageHandlers));

			MessageRouteMap = new Dictionary<Type, ISocialModelPayloadHandler<TRemoteClientHubInterfaceType>>();

			foreach (var handler in messageHandlers)
				MessageRouteMap[handler.PayloadType] = handler;
		}

		public bool CanHandle(NetworkIncomingMessage<BaseSocialModel> message)
		{
			return MessageRouteMap.ContainsKey(message.Payload.GetType());
		}

		public Task<bool> TryHandleMessage(HubConnectionMessageContext<TRemoteClientHubInterfaceType> context, NetworkIncomingMessage<BaseSocialModel> message)
		{
			return MessageRouteMap[message.Payload.GetType()].TryHandleMessage(context, message);
		}
	}
}
