using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class SocialSignalRMessageRouter<TRemoteClientHubInterfaceType> : ISocialModelMessageRouter<TRemoteClientHubInterfaceType>
	{
		private Dictionary<Type, ISocialModelPayloadHandler<TRemoteClientHubInterfaceType>> MessageRouteMap { get; }

		private ILogger<SocialSignalRMessageRouter<TRemoteClientHubInterfaceType>> Logger { get; }

		public SocialSignalRMessageRouter([JetBrains.Annotations.NotNull] IEnumerable<ISocialModelPayloadHandler<TRemoteClientHubInterfaceType>> messageHandlers,
			[JetBrains.Annotations.NotNull] ILogger<SocialSignalRMessageRouter<TRemoteClientHubInterfaceType>> logger)
		{
			if (messageHandlers == null) throw new ArgumentNullException(nameof(messageHandlers));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
			if(CanHandle(message))
				return MessageRouteMap[message.Payload.GetType()].TryHandleMessage(context, message);

			if(Logger.IsEnabled(LogLevel.Error))
				Logger.LogError($"Unable to handle Social Model of Type: {message.Payload.GetType().Name}");

			return Task.FromResult(false);
		}
	}
}
