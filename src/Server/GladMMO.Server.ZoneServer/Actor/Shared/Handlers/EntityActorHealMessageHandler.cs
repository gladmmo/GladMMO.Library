using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityActorHealMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, HealEntityActorCurrentHealthMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, HealEntityActorCurrentHealthMessage message)
		{
			//Increases health clamped by current max health.
			if (state.EntityData.DataSetIndicationArray.Get((int) EntityObjectField.UNIT_FIELD_HEALTH))
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_HEALTH, Math.Min(state.EntityData.GetFieldValue<int>(EntityObjectField.UNIT_FIELD_MAXHEALTH), state.EntityData.GetFieldValue<int>(EntityObjectField.UNIT_FIELD_HEALTH) + message.HealAmount));
		}
	}
}
