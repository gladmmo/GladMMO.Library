using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityPendingSpellCastFinishedWaitingMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, PendingSpellCastFinishedWaitingMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, PendingSpellCastFinishedWaitingMessage message)
		{
			//This indicates that the spell cast has finished.
			state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, 0);

			messageContext.Entity.TellSelf(new ImmediateCastSpellMessage(message.Pending.SpellId, message.Pending.SnapshotEntityTarget));
		}
	}
}
