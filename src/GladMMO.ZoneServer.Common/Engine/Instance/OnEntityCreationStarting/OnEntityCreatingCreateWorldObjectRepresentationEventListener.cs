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
	[AdditionalRegisterationAs(typeof(IEntityWorldRepresentationCreatedEventSubscribable))]
	public abstract class SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>, IEntityWorldRepresentationCreatedEventSubscribable
	{
		public event EventHandler<EntityWorldRepresentationCreatedEventArgs> OnEntityWorldRepresentationCreated;

		private IFactoryCreatable<GameObject, EntityPrefab> PrefabFactory { get; }

		//Don't use MAPPABLE, initialization order will mean MAYBE it doesn't exist yet.
		private IWorldTransformFactory WorldTransformFactory { get; }

		private ILog Logger { get; }

		protected SharedOnEntityCreatingCreateWorldObjectRepresentationEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IFactoryCreatable<GameObject, EntityPrefab> prefabFactory,
			[NotNull] ILog logger,
			[NotNull] IWorldTransformFactory worldTransformFactory)
			: base(subscriptionService)
		{
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			WorldTransformFactory = worldTransformFactory ?? throw new ArgumentNullException(nameof(worldTransformFactory));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			switch (args.EntityGuid.TypeId)
			{
				case EntityTypeId.TYPEID_OBJECT:
				case EntityTypeId.TYPEID_ITEM:
				case EntityTypeId.TYPEID_CONTAINER:
					return;
				case EntityTypeId.TYPEID_DYNAMICOBJECT:
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Tried to create World Representation for: {args.EntityGuid.TypeId} Id: {args.EntityGuid}. Not implemented.");
					return;

				//We implemented handling for these so far.
				case EntityTypeId.TYPEID_GAMEOBJECT:
				case EntityTypeId.TYPEID_UNIT:
				case EntityTypeId.TYPEID_PLAYER:
				case EntityTypeId.TYPEID_CORPSE:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			EntityPrefab prefabType = ComputePrefabType(args.EntityGuid);
			WorldTransform worldTransform = WorldTransformFactory.Create(args.EntityGuid);

			//load the entity's prefab from the factory
			GameObject prefab = PrefabFactory.Create(prefabType);
			GameObject entityGameObject = GameObject.Instantiate(prefab, new Vector3(worldTransform.PositionX, worldTransform.PositionY, worldTransform.PositionZ), Quaternion.Euler(0.0f, worldTransform.YAxisRotation, 0.0f));

			OnEntityWorldRepresentationCreated?.Invoke(this, new EntityWorldRepresentationCreatedEventArgs(args.EntityGuid, entityGameObject));
		}

		protected abstract EntityPrefab ComputePrefabType(ObjectGuid entityGuid);
	}
}
