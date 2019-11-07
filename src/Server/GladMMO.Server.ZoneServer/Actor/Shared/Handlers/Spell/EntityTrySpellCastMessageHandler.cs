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

		private IPendingSpellCastFactory PendingSpellFactory { get; }

		public EntityTrySpellCastMessageHandler([NotNull] INetworkTimeService timeService,
			[NotNull] IEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable,
			[NotNull] IPendingSpellCastFactory pendingSpellFactory)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
			PendingSpellFactory = pendingSpellFactory ?? throw new ArgumentNullException(nameof(pendingSpellFactory));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, TryCastSpellMessage message)
		{
			//TODO: Handle "already casting" state.
			PendingSpellCastData castData = PendingSpellFactory.Create(new PendingSpellCastCreationContext(message.SpellId));

			if (PendingSpellCastMappable.ContainsKey(state.EntityGuid))
			{
				PendingSpellCastMappable[state.EntityGuid].PendingCancel.Cancel();
				PendingSpellCastMappable.ReplaceObject(state.EntityGuid, castData);
			}
			else
				PendingSpellCastMappable.AddObject(state.EntityGuid, castData);


			//This is just a debug/testing implementation.
			//TODO: Eventually we need a pending cast system.
			//TODO: Check they are not casting or can cast the spell.
			//These must both be set at the same time, to avoid dispatching in seperate field updates.
			lock (state.EntityData.SyncObj)
			{
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, message.SpellId);
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_STARTTIME, TimeService.CurrentLocalTime);
			}

			//Instant casts can just directly send.
			if (castData.isInstantCast)
				messageContext.Entity.TellSelf(new PendingSpellCastFinishedWaitingMessage(castData));
			else
				messageContext.ContinuationScheduler.ScheduleTellOnce(castData.CastTime, messageContext.Entity, new PendingSpellCastFinishedWaitingMessage(castData), messageContext.Entity);
		}
	}
}
