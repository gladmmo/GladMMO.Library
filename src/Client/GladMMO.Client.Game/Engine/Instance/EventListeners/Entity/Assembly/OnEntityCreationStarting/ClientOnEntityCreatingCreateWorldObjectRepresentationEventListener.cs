using System;
using System.Collections.Generic;
using System.Text;
using Fasterflect;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//Conceptually this is like a partial factory
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientOnEntityCreatingCreateWorldObjectRepresentationEventListener : SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener, IEntityWorldRepresentationCreatedEventSubscribable
	{
		private ICharacterDataRepository CharacterDataRepository { get; }

		public ClientOnEntityCreatingCreateWorldObjectRepresentationEventListener(IEntityCreationStartingEventSubscribable subscriptionService, 
			IFactoryCreatable<GameObject, EntityPrefab> prefabFactory, 
			IReadonlyEntityGuidMappable<IMovementData> movementDataMappable, 
			ICharacterDataRepository characterDataRepository) 
			: base(subscriptionService, prefabFactory, movementDataMappable)
		{
			CharacterDataRepository = characterDataRepository;
		}

		protected override EntityPrefab ComputePrefabType(NetworkEntityGuid entityGuid)
		{
			switch(entityGuid.EntityType)
			{
				case EntityType.Player:
					//It could be remote player or local player.
					if(entityGuid.EntityId == CharacterDataRepository.CharacterId)
						return EntityPrefab.LocalPlayer;
					else
						return EntityPrefab.RemotePlayer;

				case EntityType.Creature:
					return EntityPrefab.NetworkNpc;
			}

			//TODO: Handle other cases.
			throw new NotSupportedException($"TODO: Implement support for: {entityGuid} creation.");
		}
	}
}
