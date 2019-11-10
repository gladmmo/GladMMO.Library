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
	public sealed class PlayerEntityActorSelectedMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, EntityActorSelectedMessage>
	{
		private ILog Logger { get; }

		public PlayerEntityActorSelectedMessageHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, EntityActorSelectedMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.SelectorGuid} Interacted with Player: {state.EntityGuid}");

			//Just tell whatever selected us that they should now target us.
			messageContext.Sender.Tell(new SetEntityActorTargetMessage(state.EntityGuid));

			//TODO: This is just for testing
			messageContext.Entity.TellSelf(new DamageEntityActorCurrentHealthMessage(1));
		}
	}
}
