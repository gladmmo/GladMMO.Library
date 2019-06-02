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

		private IFactoryCreatable<GameObject, EntityPrefab> PrefabFactory { get; }

		/// <inheritdoc />
		public DefaultEntityFactory(
			ILog logger,
			IEntityGuidMappable<GameObject> guidToGameObjectMappable,
			IGameObjectToEntityMappable gameObjectToEntityMap,
			IFactoryCreatable<GameObject, EntityPrefab> prefabFactory)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GuidToGameObjectMappable = guidToGameObjectMappable ?? throw new ArgumentNullException(nameof(guidToGameObjectMappable));
			GameObjectToEntityMap = gameObjectToEntityMap ?? throw new ArgumentNullException(nameof(gameObjectToEntityMap));
			PrefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
		}

		/// <inheritdoc />
		public GameObject Create(TCreationContext context)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Creating entity. Type: {context.EntityGuid.EntityType} Id: {context.EntityGuid.EntityId}");

			//load the entity's prefab from the factory
			GameObject prefab = PrefabFactory.Create(context.PrefabType);

			GameObject entityGameObject = GameObject.Instantiate(prefab, context.InitialPosition, Quaternion.Euler(0, 0, 0));

			GameObjectToEntityMap.ObjectToEntityMap.Add(entityGameObject, context.EntityGuid);

			GuidToGameObjectMappable.Add(context.EntityGuid, entityGameObject);

			return entityGameObject;
		}
	}
}
