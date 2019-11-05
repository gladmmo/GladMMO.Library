using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityPendingSpellCastFinishedWaitingMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, PendingSpellCastFinishedWaitingMessage>
	{
		private IReadonlyEntityGuidMappable<PendingSpellCastData> PendingSpellCastMappable { get; }

		public EntityPendingSpellCastFinishedWaitingMessageHandler([NotNull] IEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable)
		{
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, PendingSpellCastFinishedWaitingMessage message)
		{
			//This is the CURRENT pending cast.
			PendingSpellCastData castData = PendingSpellCastMappable.RetrieveEntity(state.EntityGuid);

			//TODO: Handle the canceled casts. This i NOT how.
			if (castData.StartTime != message.Pending.StartTime)
				return;

			//This indicates that the spell cast has finished.
			state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, 0);

			messageContext.Entity.Tell(new ImmediateCastSpellMessage(message.Pending.SpellId));
		}
	}
}
