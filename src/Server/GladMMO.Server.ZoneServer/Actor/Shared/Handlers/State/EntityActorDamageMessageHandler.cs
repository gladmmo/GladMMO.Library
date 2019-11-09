using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityActorDamageMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, DamageEntityActorCurrentHealthMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, DamageEntityActorCurrentHealthMessage message)
		{
			//Simple, if the entity has health we just try to subtract from it.
			if (state.EntityData.DataSetIndicationArray.Get((int) EntityObjectField.UNIT_FIELD_HEALTH))
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_HEALTH, Math.Max(0, state.EntityData.GetFieldValue<int>(EntityObjectField.UNIT_FIELD_HEALTH) - message.Damage));
		}
	}
}
