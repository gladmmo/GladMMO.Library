using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class EntityFieldDataCollectionEntityGuidMappable : IEntityGuidMappable<IChangeTrackableEntityDataCollection>, IReadonlyEntityGuidMappable<IEntityDataFieldContainer>
	{
		private ConcurrentDictionary<NetworkEntityGuid, IChangeTrackableEntityDataCollection> InternalMap { get; }

		public EntityFieldDataCollectionEntityGuidMappable()
		{
			InternalMap = new ConcurrentDictionary<NetworkEntityGuid, IChangeTrackableEntityDataCollection>();
		}

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, IChangeTrackableEntityDataCollection>.ContainsKey(NetworkEntityGuid key)
		{
			return InternalMap.ContainsKey(key);
		}

		IEntityDataFieldContainer IReadonlyEntityGuidMappable<NetworkEntityGuid, IEntityDataFieldContainer>.this[NetworkEntityGuid key] => InternalMap[key];

		public void Add(NetworkEntityGuid key, IChangeTrackableEntityDataCollection value)
		{
			InternalMap.TryAdd(key, value);
		}

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, IEntityDataFieldContainer>.ContainsKey(NetworkEntityGuid key)
		{
			return InternalMap.ContainsKey(key);
		}

		public IChangeTrackableEntityDataCollection this[NetworkEntityGuid key]
		{
			get => InternalMap[key];
			set => InternalMap[key] = value;
		}

		IEnumerator<IEntityDataFieldContainer> IEnumerable<IEntityDataFieldContainer>.GetEnumerator()
		{
			return InternalMap.Values.GetEnumerator();
		}

		IEnumerator<IChangeTrackableEntityDataCollection> IEnumerable<IChangeTrackableEntityDataCollection>.GetEnumerator()
		{
			return InternalMap.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return InternalMap.Values.GetEnumerator();
		}

		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			return InternalMap.TryRemove(entityGuid, out var result);
		}
	}
}
