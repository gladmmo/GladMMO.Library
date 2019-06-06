using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultEntityDestructor : IObjectDestructorable<NetworkEntityGuid>
	{
		private IReadonlyEntityGuidMappable<GameObject> GuidToGameObjectMappable { get; }

		private IGameObjectToEntityMappable GameObjectToEntityMap { get; }

		private IReadOnlyCollection<IEntityCollectionRemovable> RemovableCollections { get; }

		/// <inheritdoc />
		public DefaultEntityDestructor(
			IReadonlyEntityGuidMappable<GameObject> guidToGameObjectMappable,
			IGameObjectToEntityMappable gameObjectToEntityMap,
			[NotNull] IReadOnlyCollection<IEntityCollectionRemovable> removableCollections)
		{
			GuidToGameObjectMappable = guidToGameObjectMappable ?? throw new ArgumentNullException(nameof(guidToGameObjectMappable));
			GameObjectToEntityMap = gameObjectToEntityMap ?? throw new ArgumentNullException(nameof(gameObjectToEntityMap));
			RemovableCollections = removableCollections ?? throw new ArgumentNullException(nameof(removableCollections));
		}

		/// <inheritdoc />
		public bool Destroy(NetworkEntityGuid obj)
		{
			//This removes the world entity from it's special collection AND removes it from the relevant map
			GameObject rootEntityGameObject = GuidToGameObjectMappable.RetrieveEntity(obj);
			GameObjectToEntityMap.ObjectToEntityMap.Remove(rootEntityGameObject);
			GameObject.Destroy(rootEntityGameObject);

			foreach (var removable in RemovableCollections)
			{
				ProjectVersionStage.AssertBeta();
				//TODO: Should we check this?
				removable.RemoveEntityEntry(obj);
			}

			return true;
		}

		/// <inheritdoc />
		public bool OwnsEntityToDestruct(int connectionId)
		{
			throw new NotSupportedException($"TODO: We shouldn't have the destructor actually check this.");
		}
	}
}