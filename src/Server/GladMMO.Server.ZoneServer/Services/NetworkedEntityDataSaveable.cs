using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public sealed class NetworkedEntityDataSaveable : IEntityDataSaveable
	{
		private IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> MovementDataMap { get; }

		private IReadonlyEntityGuidMappable<EntitySaveableConfiguration> PersistenceConfiguration { get; }

		private IReadonlyEntityGuidMappable<EntityFieldDataCollection> EntityDataMappable { get; }

		private IZonePersistenceServiceQueueable ZonePersistenceQueueable { get; }

		private IReadOnlyCollection<IEntityCollectionRemovable> DataCollections { get; }

		public NetworkedEntityDataSaveable([NotNull] IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementDataMap, 
			[NotNull] IReadonlyEntityGuidMappable<EntitySaveableConfiguration> persistenceConfiguration, 
			[NotNull] IReadonlyEntityGuidMappable<EntityFieldDataCollection> entityDataMappable, 
			[NotNull] IZonePersistenceServiceQueueable zonePersistenceQueueable,
			[NotNull] IReadOnlyCollection<IEntityCollectionRemovable> dataCollections)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			PersistenceConfiguration = persistenceConfiguration ?? throw new ArgumentNullException(nameof(persistenceConfiguration));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			ZonePersistenceQueueable = zonePersistenceQueueable ?? throw new ArgumentNullException(nameof(zonePersistenceQueueable));
			DataCollections = dataCollections ?? throw new ArgumentNullException(nameof(dataCollections));
		}

		/// <inheritdoc />
		public void Save(ObjectGuid guid)
		{
			SaveAsync(guid).ConfigureAwaitFalseVoid().GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public async Task SaveAsync(ObjectGuid guid)
		{
			//We can only handle players at the moment, not sure how NPC data would be saved.
			if(guid.EntityType != EntityType.Player)
				return;

			//Player ALWAYS has this existing.
			EntitySaveableConfiguration saveConfig = PersistenceConfiguration.RetrieveEntity(guid);
			EntityFieldDataCollection entityData = EntityDataMappable.RetrieveEntity(guid);

			await ZonePersistenceQueueable.SaveFullCharacterDataAsync(guid.EntityId, new FullCharacterDataSaveRequest(true, saveConfig.isCurrentPositionSaveable, CreatedLocationSaveData(guid), entityData));

			//We cleanup player data on the zoneserver in a different place
			//here, because we needed it until this very last moment.
			foreach (var ed in DataCollections)
				ed.RemoveEntityEntry(guid);
		}

		private ZoneServerCharacterLocationSaveRequest CreatedLocationSaveData([NotNull] ObjectGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			//TODO: Check that the entity actually exists
			IMovementGenerator<GameObject> movementData = MovementDataMap.RetrieveEntity(guid);

			return new ZoneServerCharacterLocationSaveRequest(movementData.CurrentPosition);
		}
	}
}
