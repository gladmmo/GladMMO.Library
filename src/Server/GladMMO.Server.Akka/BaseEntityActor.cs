﻿using System;
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
	public abstract class BaseEntityActor<TChildActorType, TActorStateType> : Akka.Actor.UntypedActor, IEntityActor, IEntityActorStateInitializable<TActorStateType>
		where TActorStateType : class, IEntityActorStateContainable
		where TChildActorType : BaseEntityActor<TChildActorType, TActorStateType>
	{
		private readonly object SyncObj = new object();

		/// <summary>
		/// Potentially mutable state for the actor.
		/// </summary>
		protected TActorStateType ActorState { get; private set; }

		private IEntityActorMessageRouteable<TChildActorType, TActorStateType> MessageRouter { get; }

		protected ILog Logger { get; }

		public bool isInitialized { get; private set; } = false;

		protected BaseEntityActor(IEntityActorMessageRouteable<TChildActorType, TActorStateType> messageRouter, ILog logger)
		{
			MessageRouter = messageRouter ?? throw new ArgumentNullException(nameof(messageRouter));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected sealed override void OnReceive(object message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			if (!isInitialized)
			{
				if (ExtractPotentialStateMessage(message, out var initMessage))
					InitializeState(initMessage.State);
				else
				{
					if(Logger.IsWarnEnabled)
						Logger.Warn($"EntityActor encountered MessageType: {message.GetType().Name} before INITIALIZATION.");

					return;
				}
			}

			EntityActorMessage castedMessage = (EntityActorMessage)message;
			EntityActorMessageContext context = new EntityActorMessageContext(Sender, Self);

			if(!MessageRouter.RouteMessage(context, ActorState, castedMessage))
				if(Logger.IsWarnEnabled)
					Logger.Warn($"EntityActor encountered unhandled MessageType: {message.GetType().Name}");
		}

		protected virtual bool ExtractPotentialStateMessage(object message, out EntityActorStateInitializeMessage<TActorStateType> entityActorStateInitializeMessage)
		{
			if (message is EntityActorStateInitializeMessage<TActorStateType> initMessage)
			{
				entityActorStateInitializeMessage = initMessage;
				return true;
			}

			entityActorStateInitializeMessage = null;
			return false;
		}

		//This is never called publicly, but I used an interface because I didn't fully grasp Akka when I wrote this.
		public void InitializeState(TActorStateType state)
		{
			lock (SyncObj)
			{
				ActorState = state;
				isInitialized = true;
			}
		}
	}
}