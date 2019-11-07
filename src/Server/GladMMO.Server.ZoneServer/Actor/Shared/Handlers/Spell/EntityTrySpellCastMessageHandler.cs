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

		private ISpellTargetValidator TargetValidator { get; }

		public EntityTrySpellCastMessageHandler([NotNull] INetworkTimeService timeService,
			[NotNull] IEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable,
			[NotNull] IPendingSpellCastFactory pendingSpellFactory,
			[NotNull] ISpellTargetValidator targetValidator)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
			PendingSpellFactory = pendingSpellFactory ?? throw new ArgumentNullException(nameof(pendingSpellFactory));
			TargetValidator = targetValidator ?? throw new ArgumentNullException(nameof(targetValidator));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, TryCastSpellMessage message)
		{
			if (PendingSpellCastMappable.ContainsKey(state.EntityGuid))
			{
				PendingSpellCastData pendingCast = PendingSpellCastMappable.RetrieveEntity(state.EntityGuid);

				if (!pendingCast.isCompleted)
				{
					//TODO: Send packet spell response to client.
					return;
				}
			}

			//TODO: Send packet spell response.
			if (!TargetValidator.isSpellTargetViable(message.SpellId, state))
				return;

			PendingSpellCastData castData = CreatePendingSpellData(state, message);

			SetCastingEntityState(state, message);

			DispatchPendingSpellCast(messageContext, castData);
		}

		private PendingSpellCastData CreatePendingSpellData(DefaultEntityActorStateContainer state, TryCastSpellMessage message)
		{
			PendingSpellCastData castData = PendingSpellFactory.Create(new PendingSpellCastCreationContext(message.SpellId, state.EntityData.GetEntityGuidValue(EntityObjectField.UNIT_FIELD_TARGET)));

			if (PendingSpellCastMappable.ContainsKey(state.EntityGuid))
				PendingSpellCastMappable.ReplaceObject(state.EntityGuid, castData);
			else
				PendingSpellCastMappable.AddObject(state.EntityGuid, castData);
			return castData;
		}

		private static void DispatchPendingSpellCast(EntityActorMessageContext messageContext, PendingSpellCastData castData)
		{
			//Instant casts can just directly send.
			if (castData.isInstantCast)
				messageContext.Entity.TellSelf(new PendingSpellCastFinishedWaitingMessage(castData));
			else
				messageContext.ContinuationScheduler.ScheduleTellOnce(castData.CastTime, messageContext.Entity, new PendingSpellCastFinishedWaitingMessage(castData), messageContext.Entity);
		}

		private void SetCastingEntityState(DefaultEntityActorStateContainer state, TryCastSpellMessage message)
		{
			//This is just a debug/testing implementation.
			//TODO: Eventually we need a pending cast system.
			//TODO: Check they are not casting or can cast the spell.
			//These must both be set at the same time, to avoid dispatching in seperate field updates.
			lock (state.EntityData.SyncObj)
			{
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, message.SpellId);
				state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_STARTTIME, TimeService.CurrentLocalTime);
			}
		}
	}
}
