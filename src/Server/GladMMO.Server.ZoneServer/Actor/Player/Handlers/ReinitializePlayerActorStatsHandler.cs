using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//TODO: Move the creature to different handling, they aren't or shouldn't be like players.
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class ReinitializePlayerActorStatsHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, ReinitializeEntityActorStatsMessage>
	{
		private IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable> EntityBaseStatsFactory { get; }

		public ReinitializePlayerActorStatsHandler([NotNull] IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable> entityBaseStatsFactory)
		{
			EntityBaseStatsFactory = entityBaseStatsFactory ?? throw new ArgumentNullException(nameof(entityBaseStatsFactory));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, ReinitializeEntityActorStatsMessage message)
		{
			EntityBaseStatsModel baseStats = GenerateEntityBaseStats(state.EntityGuid, state.EntityData);

			//TODO: Do base eventually. Right now we're only doing regular health fields since that's all we deal with clientside atm.
			state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_HEALTH, baseStats.BaseHealth);
			state.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_MAXHEALTH, baseStats.BaseHealth);
		}

		private EntityBaseStatsModel GenerateEntityBaseStats([NotNull] ObjectGuid entityGuid, [NotNull] IEntityDataFieldContainer data)
		{
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));
			if (data == null) throw new ArgumentNullException(nameof(data));

			//TODO: Replace base stats component.
			EntityBaseStatsModel baseStats = EntityBaseStatsFactory.Create(new EntityDataStatsDerivable(entityGuid.EntityType, data.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL)));

			return baseStats;
		}
	}
}
