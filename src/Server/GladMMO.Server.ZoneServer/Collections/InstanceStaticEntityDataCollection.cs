﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//TODO: Verify against entity type.
	/// <summary>
	/// <see cref="IEntityGuidMappable{TValue}"/> implementation that shares static <typeparamref name="TInstanceLinkedDataType"/> component
	/// data across all <see cref="NetworkEntityGuid"/> that share an entry.
	/// </summary>
	/// <typeparam name="TInstanceLinkedDataType">The linked/shared component data type.</typeparam>
	public sealed class InstanceStaticEntityDataCollection<TInstanceLinkedDataType> : IEntityGuidMappable<TInstanceLinkedDataType>
	{
		protected Dictionary<int, TInstanceLinkedDataType> InstanceLinkedDataMap { get; }

		public InstanceStaticEntityDataCollection()
		{
			InstanceLinkedDataMap = new Dictionary<int, TInstanceLinkedDataType>();
		}

		public bool ContainsKey(NetworkEntityGuid key)
		{
			return InstanceLinkedDataMap.ContainsKey(key.EntryId);
		}

		public void Add(NetworkEntityGuid key, TInstanceLinkedDataType value)
		{
			InstanceLinkedDataMap.Add(key.EntryId, value);
		}

		public TInstanceLinkedDataType this[NetworkEntityGuid key]
		{
			get => InstanceLinkedDataMap[key.EntryId];
			set => InstanceLinkedDataMap[key.EntryId] = value;
		}

		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			return InstanceLinkedDataMap.Remove(entityGuid.EntryId);
		}

		public bool TryGetValue(NetworkEntityGuid key, out TInstanceLinkedDataType value)
		{
			return this.InstanceLinkedDataMap.TryGetValue(key.EntryId, out value);
		}

		public IEnumerator<TInstanceLinkedDataType> GetEnumerator()
		{
			return InstanceLinkedDataMap.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
