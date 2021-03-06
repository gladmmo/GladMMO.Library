﻿using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultWorldActor : BaseEntityActor<DefaultWorldActor, WorldActorState>
	{
		public DefaultWorldActor(IEntityActorMessageRouteable<DefaultWorldActor, WorldActorState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}

		protected override bool ExtractPotentialStateMessage(object message, out EntityActorStateInitializeMessage<WorldActorState> entityActorStateInitializeMessage)
		{
			bool result = base.ExtractPotentialStateMessage(message, out entityActorStateInitializeMessage);

			if (result)
			{
				entityActorStateInitializeMessage.State.WorldActorFactory = Context;
				entityActorStateInitializeMessage.State.DeathWatchService = Context;
				return result;
			}

			return false;
		}

		protected override SupervisorStrategy SupervisorStrategy()
		{
			return new OneForOneStrategy(0, -1, exception =>
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"World Actor CRITICAL FAILURE STOPPING: {exception.Message}\n\nStack: {exception.StackTrace}");

				return Directive.Escalate;
			});
		}

		protected override void OnInitialized()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"WorldActor state initialized.");
		}
	}
}
