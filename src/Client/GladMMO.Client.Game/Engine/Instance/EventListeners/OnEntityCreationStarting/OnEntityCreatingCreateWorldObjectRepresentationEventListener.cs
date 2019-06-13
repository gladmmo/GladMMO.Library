using System;
using System.Collections.Generic;
using System.Text;
using Fasterflect;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//Conceptually this is like a partial factory
	[AdditionalRegisterationAs(typeof(IEntityWorldRepresentationCreatedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnEntityCreatingCreateWorldObjectRepresentationEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>, IEntityWorldRepresentationCreatedEventSubscribable
	{
		public event EventHandler<EntityWorldRepresentationCreatedEventArgs> OnEntityWorldRepresentationCreated;

		private IFactoryCreatable<GameObject, EntityPrefab> PrefabFactory { get; }

		private ICharacterDataRepository CharacterDataRepository { get; }

		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		public OnEntityCreatingCreateWorldObjectRepresentationEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IFactoryCreatable<GameObject, EntityPrefab> prefabFactory,
			[NotNull] ICharacterDataRepository characterDataRepository,
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementDataMappable)
			: base(subscriptionService)
		{
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			CharacterDataRepository = characterDataRepository ?? throw new ArgumentNullException(nameof(characterDataRepository));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			EntityPrefab prefabType = ComputePrefabType(args.EntityGuid);
			IMovementData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			//load the entity's prefab from the factory
			GameObject prefab = PrefabFactory.Create(prefabType);
			GameObject entityGameObject = GameObject.Instantiate(prefab, movementData.InitialPosition, Quaternion.Euler(0, 0, 0));

			OnEntityWorldRepresentationCreated?.Invoke(this, new EntityWorldRepresentationCreatedEventArgs(args.EntityGuid, entityGameObject));
		}

		private EntityPrefab ComputePrefabType(NetworkEntityGuid entityGuid)
		{
			if (entityGuid.EntityType == EntityType.Player)
			{
				//It could be remote player or local player.
				if (entityGuid.EntityId == CharacterDataRepository.CharacterId)
					return EntityPrefab.LocalPlayer;
				else
					return EntityPrefab.RemotePlayer;
			}

			//TODO: Handle other cases.
			throw new NotSupportedException($"TODO: Implement support for: {entityGuid} creation.");
		}
	}
}
