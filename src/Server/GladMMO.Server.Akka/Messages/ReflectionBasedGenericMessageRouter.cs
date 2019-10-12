using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class ReflectionBasedGenericMessageRouter<TEntityActorType, TEntityActorStateType> : IEntityActorMessageRouteable<TEntityActorType, TEntityActorStateType> 
		where TEntityActorStateType : IEntityActorStateContainable
	{
		private Dictionary<Type, IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>> EntityHandlerMap { get; }

		private ILog Logger { get; }

		public ReflectionBasedGenericMessageRouter(IEnumerable<IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>> messageHandlers, ILog logger)
		{
			if (messageHandlers == null) throw new ArgumentNullException(nameof(messageHandlers));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			if(Logger.IsInfoEnabled)
				Logger.Info($"ReflectionBasedGenericMessageRouter<{typeof(TEntityActorType).Name}, {typeof(TEntityActorStateType)}> handler count: {messageHandlers.Count()}");

			EntityHandlerMap = new Dictionary<Type, IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>>(10);

			foreach (var handler in messageHandlers)
			{
				foreach(EntityActorMessageHandlerAttribute actorHandlerAttribute in handler.GetType().GetCustomAttributes<EntityActorMessageHandlerAttribute>(true))
				{
					if (actorHandlerAttribute.TargetActorType == typeof(TEntityActorType))
					{
						if(Logger.IsInfoEnabled)
							Logger.Info($"Registering: {handler.GetType().Name} for Actor: {actorHandlerAttribute.TargetActorType}");

						EntityHandlerMap[handler.MessageType] = handler;
						break;
					}
				}
			}
		}

		public bool RouteMessage(EntityActorMessageContext messageContext, TEntityActorStateType state, EntityActorMessage message)
		{
			if (messageContext == null) throw new ArgumentNullException(nameof(messageContext));
			if (state == null) throw new ArgumentNullException(nameof(state));
			if (message == null) throw new ArgumentNullException(nameof(message));

			if (EntityHandlerMap.ContainsKey(message.GetType()))
			{
				EntityHandlerMap[message.GetType()].HandleMessage(messageContext, state, message);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
