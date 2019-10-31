using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

		public NetworkedEntityDataSaveable([NotNull] IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementDataMap, 
			[NotNull] IReadonlyEntityGuidMappable<EntitySaveableConfiguration> persistenceConfiguration, 
			[NotNull] IReadonlyEntityGuidMappable<EntityFieldDataCollection> entityDataMappable, 
			[NotNull] IZonePersistenceServiceQueueable zonePersistenceQueueable)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			PersistenceConfiguration = persistenceConfiguration ?? throw new ArgumentNullException(nameof(persistenceConfiguration));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			ZonePersistenceQueueable = zonePersistenceQueueable ?? throw new ArgumentNullException(nameof(zonePersistenceQueueable));
		}

		/// <inheritdoc />
		public void Save(NetworkEntityGuid guid)
		{
			SaveAsync(guid).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public async Task SaveAsync(NetworkEntityGuid guid)
		{
			//We can only handle players at the moment, not sure how NPC data would be saved.
			if(guid.EntityType != EntityType.Player)
				return;

			//Player ALWAYS has this existing.
			EntitySaveableConfiguration saveConfig = PersistenceConfiguration.RetrieveEntity(guid);
			EntityFieldDataCollection entityData = EntityDataMappable.RetrieveEntity(guid);

			await ZonePersistenceQueueable.SaveFullCharacterDataAsync(guid.EntityId, new FullCharacterDataSaveRequest(true, saveConfig.isCurrentPositionSaveable, CreatedLocationSaveData(guid), entityData));
		}

		private ZoneServerCharacterLocationSaveRequest CreatedLocationSaveData([NotNull] NetworkEntityGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			//TODO: Check that the entity actually exists
			IMovementGenerator<GameObject> movementData = MovementDataMap.RetrieveEntity(guid);

			return new ZoneServerCharacterLocationSaveRequest(movementData.CurrentPosition);
		}
	}
}
