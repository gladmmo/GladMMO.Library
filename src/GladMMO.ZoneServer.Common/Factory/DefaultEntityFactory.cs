using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	public class DefaultEntityFactory<TCreationContext> : IFactoryCreatable<GameObject, TCreationContext>
		where TCreationContext : IEntityCreationContext
	{
		private ILog Logger { get; }

		private IEntityGuidMappable<GameObject> GuidToGameObjectMappable { get; }

		private IGameObjectToEntityMappable GameObjectToEntityMap { get; }

		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IEntityGuidMappable<IEntityDataFieldContainer> EntityFieldDataContainerMappable { get; }
		
		private IEntityGuidMappable<IChangeTrackableEntityDataCollection> EntityFieldDataChangeTrackableMappable { get; }

		private IFactoryCreatable<GameObject, EntityPrefab> PrefabFactory { get; }

		/// <inheritdoc />
		public DefaultEntityFactory(
			ILog logger,
			IEntityGuidMappable<GameObject> guidToGameObjectMappable,
			IGameObjectToEntityMappable gameObjectToEntityMap,
			IFactoryCreatable<GameObject, EntityPrefab> prefabFactory,
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable,
			[NotNull] IEntityGuidMappable<IEntityDataFieldContainer> entityFieldDataContainerMappable,
			[NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> entityFieldDataChangeTrackableMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GuidToGameObjectMappable = guidToGameObjectMappable ?? throw new ArgumentNullException(nameof(guidToGameObjectMappable));
			GameObjectToEntityMap = gameObjectToEntityMap ?? throw new ArgumentNullException(nameof(gameObjectToEntityMap));
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			EntityFieldDataContainerMappable = entityFieldDataContainerMappable ?? throw new ArgumentNullException(nameof(entityFieldDataContainerMappable));
			EntityFieldDataChangeTrackableMappable = entityFieldDataChangeTrackableMappable ?? throw new ArgumentNullException(nameof(entityFieldDataChangeTrackableMappable));
		}

		/// <inheritdoc />
		public GameObject Create(TCreationContext context)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Creating entity. Type: {context.EntityGuid.EntityType} Id: {context.EntityGuid.EntityId}");

			//load the entity's prefab from the factory
			GameObject prefab = PrefabFactory.Create(context.PrefabType);
			GameObject entityGameObject = GameObject.Instantiate(prefab, context.InitialPosition, Quaternion.Euler(0, 0, 0));

			MovementDataMappable.AddObject(context.EntityGuid, new PositionChangeMovementData(0, context.InitialPosition, Vector2.zero));
			EntityFieldDataContainerMappable.AddObject(context.EntityGuid, new ChangeTrackingEntityFieldDataCollectionDecorator(new EntityFieldDataCollection<EUnitFields>(1328)));
			EntityFieldDataChangeTrackableMappable.AddObject(context.EntityGuid, (IChangeTrackableEntityDataCollection)EntityFieldDataContainerMappable.RetrieveEntity(context.EntityGuid));
			GuidToGameObjectMappable.AddObject(context.EntityGuid, entityGameObject);

			//TODO: Rewrite the GameObject to EntityMap
			try
			{
				GameObjectToEntityMap.ObjectToEntityMap.Add(entityGameObject, context.EntityGuid);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"GameObject already exists in {nameof(GameObjectToEntityMap)}.", e);
			}

			return entityGameObject;
		}
	}
}
