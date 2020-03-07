using System; using FreecraftCore;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//TODO: Do a better implementation
	public sealed class DefaultGameObjectToEntityMappable : IReadonlyGameObjectToEntityMappable, IGameObjectToEntityMappable
	{
		/// <inheritdoc />
		public IReadOnlyDictionary<GameObject, ObjectGuid> ObjectToEntityMap => InternalMap;

		/// <inheritdoc />
		IDictionary<GameObject, ObjectGuid> IGameObjectToEntityMappable.ObjectToEntityMap => InternalMap;

		private ConcurrentDictionary<GameObject, ObjectGuid> InternalMap { get; }

		public DefaultGameObjectToEntityMappable()
		{
			InternalMap = new ConcurrentDictionary<GameObject, ObjectGuid>();
		}

		/// <inheritdoc />
		public IEnumerator<ObjectGuid> GetEnumerator()
		{
			return ObjectToEntityMap.Values.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
