using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GladMMO
{
	//TODO: Refactor interface implementation
	public sealed class MovementDataCollection : IDirtyableMovementDataCollection, IEntityGuidMappable<IMovementData>, IEntityCollectionRemovable
	{
		private IEntityGuidMappable<IMovementData> InternallyManagedMovementDictionary { get; }

		private Dictionary<NetworkEntityGuid, bool> DirtyChangesTracker { get; }

		public object SyncObject { get; } = new object();

		public MovementDataCollection()
		{
			InternallyManagedMovementDictionary = new EntityGuidDictionary<IMovementData>();
			DirtyChangesTracker = new Dictionary<NetworkEntityGuid, bool>();
		}

		/// <inheritdoc />
		public void Add(NetworkEntityGuid key, IMovementData value)
		{
			lock (SyncObject)
			{
				DirtyChangesTracker[key] = true;
				InternallyManagedMovementDictionary.Add(key, value);
			}
		}

		/// <inheritdoc />
		public bool Remove(NetworkEntityGuid key)
		{
			lock (SyncObject)
			{
				DirtyChangesTracker.Remove(key);
				return InternallyManagedMovementDictionary.RemoveEntityEntry(key);
			}
		}

		public bool ContainsKey(NetworkEntityGuid key)
		{
			lock(SyncObject)
				return this.InternallyManagedMovementDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public IMovementData this[NetworkEntityGuid key]
		{
			get => InternallyManagedMovementDictionary[key];
			set
			{
				lock (SyncObject)
				{
					DirtyChangesTracker[key] = true;
					InternallyManagedMovementDictionary[key] = value;
				}
			}
		}

		/// <inheritdoc />
		public bool isEntryDirty(NetworkEntityGuid key)
		{
			lock(SyncObject)
				return DirtyChangesTracker.ContainsKey(key) && DirtyChangesTracker[key];
		}

		/// <inheritdoc />
		public void SetDirtyState(NetworkEntityGuid key, bool isDirty)
		{
			lock(SyncObject)
				DirtyChangesTracker[key] = isDirty;
		}

		/// <inheritdoc />
		public void ClearDirty()
		{
			lock(SyncObject)
				DirtyChangesTracker.Clear();
		}

		/// <inheritdoc />
		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			lock(SyncObject)
				return this.Remove(entityGuid);
		}

		public bool TryGetValue(NetworkEntityGuid key, out IMovementData value)
		{
			lock(SyncObject)
				return this.InternallyManagedMovementDictionary.TryGetValue(key, out value);
		}

		public IEnumerator<IMovementData> GetEnumerator()
		{
			lock(SyncObject)
				return this.InternallyManagedMovementDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			lock(SyncObject)
				return GetEnumerator();
		}
	}
}
