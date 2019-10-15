using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityActorSetTargetMessageMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, SetEntityActorTargetMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, SetEntityActorTargetMessage message)
		{
			//Simple, just set our target.
			state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_TARGET, message.TargetGuid);
		}
	}
}
