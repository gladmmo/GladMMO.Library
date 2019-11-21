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

		public ReaderWriterLockSlim SyncObject { get; } = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public MovementDataCollection()
		{
			InternallyManagedMovementDictionary = new EntityGuidDictionary<IMovementData>();
			DirtyChangesTracker = new Dictionary<NetworkEntityGuid, bool>();
		}

		/// <inheritdoc />
		public void Add(NetworkEntityGuid key, IMovementData value)
		{
			SyncObject.EnterWriteLock();
			try
			{
				DirtyChangesTracker[key] = true;
				InternallyManagedMovementDictionary.Add(key, value);
			}
			finally
			{
				SyncObject.ExitWriteLock();
			}
		}

		/// <inheritdoc />
		public bool Remove(NetworkEntityGuid key)
		{
			SyncObject.EnterWriteLock();
			try
			{
				DirtyChangesTracker.Remove(key);
				return InternallyManagedMovementDictionary.RemoveEntityEntry(key);
			}
			finally
			{
				SyncObject.ExitWriteLock();
			}
		}

		public bool ContainsKey(NetworkEntityGuid key)
		{
			SyncObject.EnterReadLock();
			try
			{
				return this.InternallyManagedMovementDictionary.ContainsKey(key);
			}
			finally
			{
				SyncObject.ExitReadLock();
			}
		}

		/// <inheritdoc />
		public IMovementData this[NetworkEntityGuid key]
		{
			get => InternallyManagedMovementDictionary[key];
			set
			{
				SyncObject.EnterWriteLock();
				try
				{
					DirtyChangesTracker[key] = true;
					InternallyManagedMovementDictionary[key] = value;
				}
				finally
				{
					SyncObject.ExitWriteLock();
				}
			}
		}

		/// <inheritdoc />
		public bool isEntryDirty(NetworkEntityGuid key)
		{
			SyncObject.EnterReadLock();
			try
			{
				return DirtyChangesTracker.ContainsKey(key) && DirtyChangesTracker[key];
			}
			finally
			{
				SyncObject.ExitReadLock();
			}
		}

		/// <inheritdoc />
		public void SetDirtyState(NetworkEntityGuid key, bool isDirty)
		{
			SyncObject.EnterWriteLock();
			try
			{
				DirtyChangesTracker[key] = isDirty;
			}
			finally
			{
				SyncObject.ExitWriteLock();
			}
		}

		/// <inheritdoc />
		public void ClearDirty()
		{
			SyncObject.EnterWriteLock();
			try
			{
				DirtyChangesTracker.Clear();
			}
			finally
			{
				SyncObject.ExitWriteLock();
			}
		}

		/// <inheritdoc />
		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			SyncObject.EnterWriteLock();
			try
			{
				return this.Remove(entityGuid);
			}
			finally
			{
				SyncObject.ExitWriteLock();
			}
		}

		public bool TryGetValue(NetworkEntityGuid key, out IMovementData value)
		{
			SyncObject.EnterReadLock();
			try
			{
				return this.InternallyManagedMovementDictionary.TryGetValue(key, out value);
			}
			finally
			{
				SyncObject.ExitReadLock();
			}
		}

		public IEnumerator<IMovementData> GetEnumerator()
		{
			return this.InternallyManagedMovementDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
