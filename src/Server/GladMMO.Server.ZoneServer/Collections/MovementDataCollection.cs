using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//TODO: Refactor interface implementation
	public sealed class MovementDataCollection : IDirtyableMovementDataCollection, IEntityGuidMappable<IMovementData>, IEntityCollectionRemovable
	{
		private IEntityGuidMappable<IMovementData> InternallyManagedMovementDictionary { get; }

		private Dictionary<NetworkEntityGuid, bool> DirtyChangesTracker { get; }

		public MovementDataCollection()
		{
			InternallyManagedMovementDictionary = new EntityGuidDictionary<IMovementData>();
			DirtyChangesTracker = new Dictionary<NetworkEntityGuid, bool>();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<NetworkEntityGuid, IMovementData>> GetEnumerator()
		{
			return InternallyManagedMovementDictionary.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)InternallyManagedMovementDictionary).GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(KeyValuePair<NetworkEntityGuid, IMovementData> item)
		{
			DirtyChangesTracker[item.Key] = true;
			InternallyManagedMovementDictionary.Add(item.Key, item.Value);
		}

		/// <inheritdoc />
		public void Clear()
		{
			DirtyChangesTracker.Clear();
			InternallyManagedMovementDictionary.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<NetworkEntityGuid, IMovementData> item)
		{
			return InternallyManagedMovementDictionary.ContainsKey(item.Key) && InternallyManagedMovementDictionary[item.Key] == item.Value;
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<NetworkEntityGuid, IMovementData>[] array, int arrayIndex)
		{
			throw new NotSupportedException($"TODO: Implement CopyTo.");
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<NetworkEntityGuid, IMovementData> item)
		{
			DirtyChangesTracker.Remove(item.Key);
			//Assume value is right
			return InternallyManagedMovementDictionary.Remove(item.Key);
		}

		/// <inheritdoc />
		int ICollection<KeyValuePair<NetworkEntityGuid, IMovementData>>.Count => InternallyManagedMovementDictionary.Count;

		/// <inheritdoc />
		bool ICollection<KeyValuePair<NetworkEntityGuid, IMovementData>>.IsReadOnly => false;

		/// <inheritdoc />
		int IReadOnlyCollection<KeyValuePair<NetworkEntityGuid, IMovementData>>.Count => InternallyManagedMovementDictionary.Count;

		/// <inheritdoc />
		public void Add(NetworkEntityGuid key, IMovementData value)
		{
			DirtyChangesTracker[key] = true;
			InternallyManagedMovementDictionary.Add(key, value);
		}

		/// <inheritdoc />
		bool IDictionary<NetworkEntityGuid, IMovementData>.ContainsKey(NetworkEntityGuid key)
		{
			return InternallyManagedMovementDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool Remove(NetworkEntityGuid key)
		{
			DirtyChangesTracker.Remove(key);
			return InternallyManagedMovementDictionary.Remove(key);
		}

		/// <inheritdoc />
		bool IDictionary<NetworkEntityGuid, IMovementData>.TryGetValue(NetworkEntityGuid key, out IMovementData value)
		{
			return InternallyManagedMovementDictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		bool IReadOnlyDictionary<NetworkEntityGuid, IMovementData>.ContainsKey(NetworkEntityGuid key)
		{
			return InternallyManagedMovementDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		bool IReadOnlyDictionary<NetworkEntityGuid, IMovementData>.TryGetValue(NetworkEntityGuid key, out IMovementData value)
		{
			return InternallyManagedMovementDictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public IMovementData this[NetworkEntityGuid key]
		{
			get => InternallyManagedMovementDictionary[key];
			set
			{
				DirtyChangesTracker[key] = true;
				InternallyManagedMovementDictionary[key] = value;
			}
		}

		/// <inheritdoc />
		IEnumerable<NetworkEntityGuid> IReadOnlyDictionary<NetworkEntityGuid, IMovementData>.Keys => InternallyManagedMovementDictionary.Keys;

		/// <inheritdoc />
		ICollection<IMovementData> IDictionary<NetworkEntityGuid, IMovementData>.Values => InternallyManagedMovementDictionary.Values;

		/// <inheritdoc />
		ICollection<NetworkEntityGuid> IDictionary<NetworkEntityGuid, IMovementData>.Keys => InternallyManagedMovementDictionary.Keys;

		/// <inheritdoc />
		IEnumerable<IMovementData> IReadOnlyDictionary<NetworkEntityGuid, IMovementData>.Values => InternallyManagedMovementDictionary.Values;

		/// <inheritdoc />
		public bool isEntryDirty(NetworkEntityGuid key)
		{
			return DirtyChangesTracker.ContainsKey(key) && DirtyChangesTracker[key];
		}

		/// <inheritdoc />
		public void SetDirtyState(NetworkEntityGuid key, bool isDirty)
		{
			DirtyChangesTracker[key] = isDirty;
		}

		/// <inheritdoc />
		public void ClearDirty()
		{
			DirtyChangesTracker.Clear();
		}

		/// <inheritdoc />
		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			return this.Remove(entityGuid);
		}
	}
}
