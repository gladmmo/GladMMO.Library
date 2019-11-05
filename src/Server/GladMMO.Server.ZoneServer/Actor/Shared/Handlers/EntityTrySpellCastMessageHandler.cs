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

		public EntityTrySpellCastMessageHandler([NotNull] INetworkTimeService timeService)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, TryCastSpellMessage message)
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
