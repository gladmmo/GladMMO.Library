using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	/// <summary>
	/// Base Akka actor type for Entities in GladMMO.
	/// </summary>
	/// <typeparam name="TActorStateType"></typeparam>
	/// <typeparam name="TChildActorType"></typeparam>
	public abstract class BaseEntityActor<TChildActorType, TActorStateType> : Akka.Actor.UntypedActor
		where TActorStateType : class, IEntityActorStateContainable
		where TChildActorType : BaseEntityActor<TChildActorType, TActorStateType>
	{
		/// <summary>
		/// Potentially mutable state for the actor.
		/// </summary>
		protected TActorStateType ActorState { get; }

		private IEntityActorMessageRouteable<TChildActorType, TActorStateType> MessageRouter { get; }

		protected ILog Logger { get; }

		protected BaseEntityActor(TActorStateType actorState, IEntityActorMessageRouteable<TChildActorType, TActorStateType> messageRouter, ILog logger)
		{
			MessageRouter = messageRouter ?? throw new ArgumentNullException(nameof(messageRouter));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ActorState = actorState ?? throw new ArgumentNullException(nameof(actorState));
		}

		protected sealed override void OnReceive(object message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			EntityActorMessage castedMessage = (EntityActorMessage)message;
			EntityActorMessageContext context = new EntityActorMessageContext(Sender, Self);

			if(!MessageRouter.RouteMessage(context, ActorState, castedMessage))
				if(Logger.IsWarnEnabled)
					Logger.Warn($"EntityActor encountered unhandled MessageType: {message.GetType().Name}");
		}
	}
}
