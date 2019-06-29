using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeEntityHealthEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable> EntityBaseStatsFactory { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityGuidMappable<EntityBaseStatsModel> EntityBaseStatsMappable { get; }

		public InitializeEntityHealthEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable> entityBaseStatsFactory,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IEntityGuidMappable<EntityBaseStatsModel> entityBaseStatsMappable) 
			: base(subscriptionService)
		{
			EntityBaseStatsFactory = entityBaseStatsFactory ?? throw new ArgumentNullException(nameof(entityBaseStatsFactory));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			EntityBaseStatsMappable = entityBaseStatsMappable ?? throw new ArgumentNullException(nameof(entityBaseStatsMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			if(args.EntityGuid.EntityType != EntityType.Player)
				throw new NotImplementedException($"TODO: Support non-players.");

			IEntityDataFieldContainer dataContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);
			EntityBaseStatsModel baseStats = GenerateEntityBaseStats(args.EntityGuid);

			//TODO: Do base eventually. Right now we're only doing regular health fields since that's all we deal with clientside atm.
			dataContainer.SetFieldValue(EUnitFields.UNIT_FIELD_HEALTH, baseStats.BaseHealth);
			dataContainer.SetFieldValue(EUnitFields.UNIT_FIELD_MAXHEALTH, baseStats.BaseHealth);
		}

		private EntityBaseStatsModel GenerateEntityBaseStats(NetworkEntityGuid entityGuid)
		{
			//We don't assume we've created the entity base stats component
			if (EntityBaseStatsMappable.ContainsKey(entityGuid))
				return EntityBaseStatsMappable.RetrieveEntity(entityGuid);

			//TODO: We should get actual level somewhere else. During creation time (before CreationStarted since it's a persisted data.
			EntityBaseStatsModel baseStats = EntityBaseStatsFactory.Create(new EntityDataStatsDerivable(EntityType.Player, 1));
			EntityBaseStatsMappable.AddObject(entityGuid, baseStats);

			return baseStats;
		}
	}
}
