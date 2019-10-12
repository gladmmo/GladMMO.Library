using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GladMMO
{
	public sealed class ReflectionBasedGenericMessageRouter<TEntityActorType, TEntityActorStateType> : IEntityActorMessageRouteable<TEntityActorType, TEntityActorStateType> 
		where TEntityActorStateType : IEntityActorStateContainable
	{
		private Dictionary<Type, IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>> EntityHandlerMap { get; }

		public ReflectionBasedGenericMessageRouter(IEnumerable<IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>> actorAssemblyDefinitions)
		{
			EntityHandlerMap = new Dictionary<Type, IEntityActorMessageHandler<TEntityActorStateType, EntityActorMessage>>(10);

			//TODO: Handle external assembly loading.
			//foreach (Assembly actorAssemblyToParse in actorAssemblyDefinitions.AssemblyNames
			//	.Select(d => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => d == a.FullName.ToString())))
			foreach (var handler in actorAssemblyDefinitions)
			{
				foreach(EntityActorMessageHandlerAttribute actorHandlerAttribute in handler.GetType().GetCustomAttributes<EntityActorMessageHandlerAttribute>())
				{
					if (actorHandlerAttribute.TargetActorType == typeof(TEntityActorType))
					{
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
