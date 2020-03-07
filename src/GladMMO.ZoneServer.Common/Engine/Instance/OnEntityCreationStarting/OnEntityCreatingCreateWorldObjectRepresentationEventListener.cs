using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Fasterflect;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//Conceptually this is like a partial factory
	[AdditionalRegisterationAs(typeof(IEntityWorldRepresentationCreatedEventSubscribable))]
	public abstract class SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>, IEntityWorldRepresentationCreatedEventSubscribable
	{
		public event EventHandler<EntityWorldRepresentationCreatedEventArgs> OnEntityWorldRepresentationCreated;

		private IFactoryCreatable<GameObject, EntityPrefab> PrefabFactory { get; }

		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		protected SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IFactoryCreatable<GameObject, EntityPrefab> prefabFactory,
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementDataMappable)
			: base(subscriptionService)
		{
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			EntityPrefab prefabType = ComputePrefabType(args.EntityGuid);
			IMovementData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			//load the entity's prefab from the factory
			GameObject prefab = PrefabFactory.Create(prefabType);
			GameObject entityGameObject = GameObject.Instantiate(prefab, movementData.InitialPosition, Quaternion.Euler(0, movementData.Rotation, 0));

			OnEntityWorldRepresentationCreated?.Invoke(this, new EntityWorldRepresentationCreatedEventArgs(args.EntityGuid, entityGameObject));
		}

		protected abstract EntityPrefab ComputePrefabType(ObjectGuid entityGuid);
	}
}
