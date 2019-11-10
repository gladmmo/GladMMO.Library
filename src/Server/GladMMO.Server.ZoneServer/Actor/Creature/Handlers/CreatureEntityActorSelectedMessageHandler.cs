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
	public sealed class CreatureEntityActorSelectedMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, EntityActorSelectedMessage>
	{
		public ILog Logger { get; }

		public CreatureEntityActorSelectedMessageHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, EntityActorSelectedMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.SelectorGuid} selected Creature: {state.EntityGuid}");

			//Make sure the creature can be selected.
			if (state.EntityData.HasBaseObjectFieldFlag(BaseObjectFieldFlags.UNIT_FLAG_NOT_SELECTABLE))
			{
				Logger.Debug($"Entity: {message.SelectorGuid} tried to select unselectable Creature: {state.EntityGuid}");
				return;
			}

			//Just tell whatever interacted with us that they should now target us.
			messageContext.Sender.Tell(new SetEntityActorTargetMessage(state.EntityGuid));
		}
	}
}
