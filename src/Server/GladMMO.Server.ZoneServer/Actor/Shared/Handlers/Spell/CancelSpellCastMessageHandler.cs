using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class CancelSpellCastMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, CancelSpellCastMessage>
	{
		private IEntityGuidMappable<PendingSpellCastData> PendingSpellCastMappable { get; }

		public CancelSpellCastMessageHandler([NotNull] IEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable)
		{
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, CancelSpellCastMessage message)
		{
			//If we have a pending cast then cancel it.
			if (PendingSpellCastMappable.ContainsKey(state.EntityGuid))
				PendingSpellCastMappable.RetrieveEntity(state.EntityGuid).PendingCancel.Cancel();

			//Just set the casting state to 0.
			state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, 0);
		}
	}
}
