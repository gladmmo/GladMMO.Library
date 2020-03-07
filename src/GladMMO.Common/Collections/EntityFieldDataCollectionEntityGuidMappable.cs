using System; using FreecraftCore;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class EntityFieldDataCollectionEntityGuidMappable : IEntityGuidMappable<IChangeTrackableEntityDataCollection>, IReadonlyEntityGuidMappable<IEntityDataFieldContainer>
	{
		private ConcurrentDictionary<ObjectGuid, IChangeTrackableEntityDataCollection> InternalMap { get; }

		public EntityFieldDataCollectionEntityGuidMappable()
		{
			InternalMap = new ConcurrentDictionary<ObjectGuid, IChangeTrackableEntityDataCollection>();
		}

		bool IReadonlyEntityGuidMappable<ObjectGuid, IChangeTrackableEntityDataCollection>.ContainsKey(ObjectGuid key)
		{
			return InternalMap.ContainsKey(key);
		}

		IEntityDataFieldContainer IReadonlyEntityGuidMappable<ObjectGuid, IEntityDataFieldContainer>.this[ObjectGuid key] => InternalMap[key];

		public void Add(ObjectGuid key, IChangeTrackableEntityDataCollection value)
		{
			InternalMap.TryAdd(key, value);
		}

		bool IReadonlyEntityGuidMappable<ObjectGuid, IEntityDataFieldContainer>.ContainsKey(ObjectGuid key)
		{
			return InternalMap.ContainsKey(key);
		}

		public IChangeTrackableEntityDataCollection this[ObjectGuid key]
		{
			get => InternalMap[key];
			set => InternalMap[key] = value;
		}

		public bool TryGetValue(ObjectGuid key, out IChangeTrackableEntityDataCollection value)
		{
			return InternalMap.TryGetValue(key, out value);
		}

		public bool TryGetValue(ObjectGuid key, out IEntityDataFieldContainer value)
		{
			bool result = InternalMap.TryGetValue(key, out var value2);
			value = value2;
			return result;
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

		public bool RemoveEntityEntry(ObjectGuid entityGuid)
		{
			return InternalMap.TryRemove(entityGuid, out var result);
		}
	}
}
