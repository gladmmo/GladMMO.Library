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

		private IZoneServerToGameServerClient ZoneToSeverClient { get; }

		private IReadonlyEntityGuidMappable<EntitySaveableConfiguration> PersistenceConfiguration { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private WorldConfiguration WorldConfig { get; }

		/// <inheritdoc />
		public NetworkedEntityDataSaveable([NotNull] IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementDataMap, 
			[NotNull] IZoneServerToGameServerClient zoneToSeverClient,
			[NotNull] IReadonlyEntityGuidMappable<EntitySaveableConfiguration> persistenceConfiguration,
			[NotNull] WorldConfiguration worldConfig,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			ZoneToSeverClient = zoneToSeverClient ?? throw new ArgumentNullException(nameof(zoneToSeverClient));
			PersistenceConfiguration = persistenceConfiguration ?? throw new ArgumentNullException(nameof(persistenceConfiguration));
			WorldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
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
			IEntityDataFieldContainer entityData = EntityDataMappable.RetrieveEntity(guid);

			if (saveConfig.isCurrentPositionSaveable)
			{
				await SavePositionAsync(guid)
					.ConfigureAwait(false);
			}

			//Save the player's experience.
			await ZoneToSeverClient.UpdatePlayerData(guid.EntityId, new CharacterDataInstance(entityData.GetFieldValue<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE)))
				.ConfigureAwait(false);

			await ZoneToSeverClient.ReleaseActiveSession(guid.EntityId)
				.ConfigureAwait(false);
		}

		private async Task SavePositionAsync(NetworkEntityGuid guid)
		{
			//TODO: Check that the entity actually exists
			IMovementGenerator<GameObject> movementData = MovementDataMap.RetrieveEntity(guid);

			//TODO: Handle map ID.
			await ZoneToSeverClient.SaveCharacterLocation(new ZoneServerCharacterLocationSaveRequest(guid.EntityId, movementData.CurrentPosition, (int)WorldConfig.WorldId))
				.ConfigureAwait(false);
		}
	}
}
