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

		/// <inheritdoc />
		public NetworkedEntityDataSaveable([NotNull] IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementDataMap, [NotNull] IZoneServerToGameServerClient zoneToSeverClient)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			ZoneToSeverClient = zoneToSeverClient ?? throw new ArgumentNullException(nameof(zoneToSeverClient));
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

			//TODO: Check that the entity actually exists
			IMovementGenerator<GameObject> movementData = MovementDataMap.RetrieveEntity(guid);

			//TODO: Handle map ID.
			await ZoneToSeverClient.SaveCharacterLocation(new ZoneServerCharacterLocationSaveRequest(guid.EntityId, movementData.CurrentPosition, 1))
				.ConfigureAwait(false);

			await ZoneToSeverClient.ReleaseActiveSession(guid.EntityId)
				.ConfigureAwait(false);
		}
	}
}
