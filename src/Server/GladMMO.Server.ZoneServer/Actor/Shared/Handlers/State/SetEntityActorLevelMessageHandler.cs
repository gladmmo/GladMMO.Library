using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultGameObjectEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	public sealed class SetEntityActorLevelMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, SetEntityActorLevelMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, SetEntityActorLevelMessage message)
		{
			//Set the level and then reinitialize entity stats.
			state.EntityData.SetFieldValue(BaseObjectField.UNIT_FIELD_LEVEL, message.NewLevel);
			messageContext.Entity.Tell(new ReinitializeEntityActorStatsMessage());
		}
	}
}
