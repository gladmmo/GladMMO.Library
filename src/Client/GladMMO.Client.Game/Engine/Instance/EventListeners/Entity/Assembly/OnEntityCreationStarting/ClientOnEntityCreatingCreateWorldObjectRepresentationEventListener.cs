using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Fasterflect;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//Conceptually this is like a partial factory
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientOnEntityCreatingCreateWorldObjectRepresentationEventListener : SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener, IEntityWorldRepresentationCreatedEventSubscribable
	{
		private ILocalCharacterDataRepository CharacterDataRepository { get; }

		public ClientOnEntityCreatingCreateWorldObjectRepresentationEventListener(IEntityCreationStartingEventSubscribable subscriptionService, 
			IFactoryCreatable<GameObject, EntityPrefab> prefabFactory, 
			ILocalCharacterDataRepository characterDataRepository,
			IWorldTransformFactory worldTransformFactory,
			ILog logger) 
			: base(subscriptionService, prefabFactory, logger, worldTransformFactory)
		{
			CharacterDataRepository = characterDataRepository;
		}

		protected override EntityPrefab ComputePrefabType(ObjectGuid entityGuid)
		{
			switch(entityGuid.TypeId)
			{
				case EntityTypeId.TYPEID_PLAYER:
					//It could be remote player or local player.
					if(entityGuid == CharacterDataRepository.LocalCharacterGuid)
						return EntityPrefab.LocalPlayer;
					else
						return EntityPrefab.RemotePlayer;
				case EntityTypeId.TYPEID_UNIT:
					return EntityPrefab.NetworkNpc;
				case EntityTypeId.TYPEID_GAMEOBJECT:
					return EntityPrefab.NetworkGameObject;
				case EntityTypeId.TYPEID_CORPSE:
					return EntityPrefab.NetworkCorpse;
			}

			//TODO: Handle other cases.
			throw new NotSupportedException($"TODO: Implement support for: {entityGuid} creation.");
		}
	}
}
