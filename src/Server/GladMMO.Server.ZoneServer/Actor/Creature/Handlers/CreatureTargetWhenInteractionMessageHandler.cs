using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.AI;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	public sealed class CreatureTargetWhenInteractionMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, InteractWithEntityActorMessage>
	{
		public ILog Logger { get; }

		public CreatureTargetWhenInteractionMessageHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, InteractWithEntityActorMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.EntityInteracting} Interacted with Creature: {state.EntityGuid}");

			//Just tell whatever interacted with us that they should now target us.
			messageContext.Sender.Tell(new SetEntityActorTargetMessage(state.EntityGuid));
		}
	}
}
