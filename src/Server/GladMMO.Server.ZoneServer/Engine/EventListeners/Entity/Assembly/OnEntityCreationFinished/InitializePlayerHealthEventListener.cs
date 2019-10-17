using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	//TODO: Refactor to consolidate with creature health initialization first.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializePlayerHealthEventListener : PlayerCreationFinishedEventListener
	{
		private IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable> EntityBaseStatsFactory { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityGuidMappable<EntityBaseStatsModel> EntityBaseStatsMappable { get; }

		public InitializePlayerHealthEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable> entityBaseStatsFactory,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IEntityGuidMappable<EntityBaseStatsModel> entityBaseStatsMappable) 
			: base(subscriptionService)
		{
			EntityBaseStatsFactory = entityBaseStatsFactory ?? throw new ArgumentNullException(nameof(entityBaseStatsFactory));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			EntityBaseStatsMappable = entityBaseStatsMappable ?? throw new ArgumentNullException(nameof(entityBaseStatsMappable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			IEntityDataFieldContainer dataContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);
			EntityBaseStatsModel baseStats = GenerateEntityBaseStats(args.EntityGuid);

			//TODO: This is kinda a hack to avoid GameObjects iniitalizing this.
			if(args.EntityGuid.EntityType == EntityType.GameObject)
				return;

			//TODO: Do base eventually. Right now we're only doing regular health fields since that's all we deal with clientside atm.
			dataContainer.SetFieldValue(EntityObjectField.UNIT_FIELD_HEALTH, baseStats.BaseHealth);
			dataContainer.SetFieldValue(EntityObjectField.UNIT_FIELD_MAXHEALTH, baseStats.BaseHealth);
		}

		private EntityBaseStatsModel GenerateEntityBaseStats(NetworkEntityGuid entityGuid)
		{
			IEntityDataFieldContainer entityData = EntityDataMappable.RetrieveEntity(entityGuid);

			//We don't assume we've created the entity base stats component
			if (EntityBaseStatsMappable.ContainsKey(entityGuid))
				return EntityBaseStatsMappable.RetrieveEntity(entityGuid);

			EntityBaseStatsModel baseStats = EntityBaseStatsFactory.Create(new EntityDataStatsDerivable(entityGuid.EntityType, entityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL)));
			EntityBaseStatsMappable.AddObject(entityGuid, baseStats);

			return baseStats;
		}
	}
}
