using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public class ServerOnEntityCreatingCreateWorldObjectRepresentationEventListener : SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener
	{
		public ServerOnEntityCreatingCreateWorldObjectRepresentationEventListener(IEntityCreationStartingEventSubscribable subscriptionService, 
			IFactoryCreatable<GameObject, EntityPrefab> prefabFactory, 
			IReadonlyEntityGuidMappable<IMovementData> movementDataMappable) 
			: base(subscriptionService, prefabFactory, movementDataMappable)
		{
		}

		protected override EntityPrefab ComputePrefabType([NotNull] NetworkEntityGuid entityGuid)
		{
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));

			switch (entityGuid.EntityType)
			{
				case EntityType.None:
					break;
				case EntityType.Player:
					return EntityPrefab.RemotePlayer;
				case EntityType.GameObject:
					return EntityPrefab.NetworkGameObject;
					break;
				case EntityType.Creature:
					return EntityPrefab.NetworkNpc;
				default:
					throw new ArgumentOutOfRangeException();
			}

			throw new InvalidOperationException($"Failed to compute {nameof(EntityPrefab)} from Guid: {entityGuid}");
		}
	}
}
