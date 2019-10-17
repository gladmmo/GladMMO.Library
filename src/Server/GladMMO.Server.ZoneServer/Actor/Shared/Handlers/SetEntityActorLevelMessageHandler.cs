using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultGameObjectEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	public sealed class SetEntityActorLevelMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, SetEntityActorLevelMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, SetEntityActorLevelMessage message)
		{
			state.EntityData.SetFieldValue(BaseObjectField.UNIT_FIELD_LEVEL, message.NewLevel);
		}
	}
}
