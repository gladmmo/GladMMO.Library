using System;
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
		public IReadOnlyDictionary<GameObject, NetworkEntityGuid> ObjectToEntityMap => InternalMap;

		/// <inheritdoc />
		IDictionary<GameObject, NetworkEntityGuid> IGameObjectToEntityMappable.ObjectToEntityMap => InternalMap;

		private ConcurrentDictionary<GameObject, NetworkEntityGuid> InternalMap { get; }

		public DefaultGameObjectToEntityMappable()
		{
			InternalMap = new ConcurrentDictionary<GameObject, NetworkEntityGuid>();
		}

		/// <inheritdoc />
		public IEnumerator<NetworkEntityGuid> GetEnumerator()
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
