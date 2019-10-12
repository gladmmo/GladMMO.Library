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

		public ReflectionBasedGenericMessageRouter(IEnumerable<IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>> messageHandler, ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			EntityHandlerMap = new Dictionary<Type, IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>>(10);

			foreach (var handler in messageHandler)
			{
				foreach(EntityActorMessageHandlerAttribute actorHandlerAttribute in handler.GetType().GetCustomAttributes<EntityActorMessageHandlerAttribute>())
				{
					if (actorHandlerAttribute.TargetActorType == typeof(TEntityActorType))
					{
						if(Logger.IsDebugEnabled)
							Logger.Debug($"Registering: {handler.GetType().Name} for Actor: {actorHandlerAttribute.TargetActorType}");

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
