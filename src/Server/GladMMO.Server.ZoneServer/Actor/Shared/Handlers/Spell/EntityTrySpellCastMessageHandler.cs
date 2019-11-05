using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityTrySpellCastMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, TryCastSpellMessage>
	{
		private IReadonlyNetworkTimeService TimeService { get; }

		private IEntityGuidMappable<PendingSpellCastData> PendingSpellCastMappable { get; }

		public EntityTrySpellCastMessageHandler([NotNull] INetworkTimeService timeService,
			[NotNull] IEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, TryCastSpellMessage message)
		{
			if(PendingSpellCastMappable.ContainsKey(state.EntityGuid))
				PendingSpellCastMappable.ReplaceObject(state.EntityGuid, new PendingSpellCastData(TimeService.CurrentLocalTime, message.SpellId));
			else
				PendingSpellCastMappable.AddObject(state.EntityGuid, new PendingSpellCastData(TimeService.CurrentLocalTime, message.SpellId));

			//This is just a debug/testing implementation.
			//TODO: Eventually we need a pending cast system.
			//TODO: Check they are not casting or can cast the spell.
			//These must both be set at the same time, to avoid dispatching in seperate field updates.
			lock (state.EntityData.SyncObj)
			{
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, message.SpellId);
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_STARTTIME, TimeService.CurrentLocalTime);
			}

			//TODO: This is demo code, we need to handle TARGETING and CANCELable casts.
			messageContext.ContinuationScheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), messageContext.Entity, new PendingSpellCastFinishedWaitingMessage(PendingSpellCastMappable.RetrieveEntity(state.EntityGuid)), messageContext.Entity);
		}
	}
}
