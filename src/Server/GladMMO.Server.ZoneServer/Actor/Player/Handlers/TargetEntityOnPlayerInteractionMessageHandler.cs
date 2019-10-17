using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class TargetEntityOnPlayerInteractionMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, InteractWithEntityActorMessage>
	{
		private ILog Logger { get; }

		public TargetEntityOnPlayerInteractionMessageHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, InteractWithEntityActorMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.EntityInteracting} Interacted with Player: {state.EntityGuid}");

			//Just tell whatever interacted with us that they should now target us.
			messageContext.Sender.Tell(new SetEntityActorTargetMessage(state.EntityGuid));

			//TODO: This is just for testing
			messageContext.Entity.Tell(new DamageEntityActorCurrentHealthMessage(1));
		}
	}
}
