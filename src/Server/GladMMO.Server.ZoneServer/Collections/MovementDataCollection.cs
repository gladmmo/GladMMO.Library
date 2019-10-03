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
		public void Add(NetworkEntityGuid key, IMovementData value)
		{
			DirtyChangesTracker[key] = true;
			InternallyManagedMovementDictionary.Add(key, value);
		}

		/// <inheritdoc />
		public bool Remove(NetworkEntityGuid key)
		{
			DirtyChangesTracker.Remove(key);
			return InternallyManagedMovementDictionary.RemoveEntityEntry(key);
		}

		public bool ContainsKey(NetworkEntityGuid key)
		{
			return this.InternallyManagedMovementDictionary.ContainsKey(key);
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
