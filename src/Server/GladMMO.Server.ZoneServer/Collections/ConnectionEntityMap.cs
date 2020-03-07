using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyConnectionEntityCollection : IReadOnlyDictionary<int, ObjectGuid>
	{

	}

	public interface IConnectionEntityCollection : IDictionary<int, ObjectGuid>
	{

	}

	public sealed class ConnectionEntityMap : IReadonlyConnectionEntityCollection, IConnectionEntityCollection
	{
		private Dictionary<int, ObjectGuid> InternallyManagedDictionary { get; }

		public ConnectionEntityMap()
		{
			InternallyManagedDictionary = new Dictionary<int, ObjectGuid>();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<int, ObjectGuid>> GetEnumerator()
		{
			return InternallyManagedDictionary.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)InternallyManagedDictionary).GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(KeyValuePair<int, ObjectGuid> item)
		{
			InternallyManagedDictionary.Add(item.Key, item.Value);
		}

		/// <inheritdoc />
		public void Clear()
		{
			InternallyManagedDictionary.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<int, ObjectGuid> item)
		{
			return InternallyManagedDictionary.ContainsKey(item.Key);
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<int, ObjectGuid>[] array, int arrayIndex)
		{
			throw new NotImplementedException($"TODO: Implement copy.");
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<int, ObjectGuid> item)
		{
			return InternallyManagedDictionary.Remove(item.Key);
		}

		/// <inheritdoc />
		int ICollection<KeyValuePair<int, ObjectGuid>>.Count => InternallyManagedDictionary.Count;

		/// <inheritdoc />
		public bool IsReadOnly => false;

		/// <inheritdoc />
		int IReadOnlyCollection<KeyValuePair<int, ObjectGuid>>.Count => InternallyManagedDictionary.Count;

		/// <inheritdoc />
		public void Add(int key, ObjectGuid value)
		{
			InternallyManagedDictionary.Add(key, value);
		}

		/// <inheritdoc />
		bool IDictionary<int, ObjectGuid>.ContainsKey(int key)
		{
			return InternallyManagedDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool Remove(int key)
		{
			return InternallyManagedDictionary.Remove(key);
		}

		/// <inheritdoc />
		bool IDictionary<int, ObjectGuid>.TryGetValue(int key, out ObjectGuid value)
		{
			return InternallyManagedDictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		bool IReadOnlyDictionary<int, ObjectGuid>.ContainsKey(int key)
		{
			return InternallyManagedDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		bool IReadOnlyDictionary<int, ObjectGuid>.TryGetValue(int key, out ObjectGuid value)
		{
			return InternallyManagedDictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public ObjectGuid this[int key]
		{
			get => InternallyManagedDictionary[key];
			set => InternallyManagedDictionary[key] = value;
		}

		/// <inheritdoc />
		IEnumerable<int> IReadOnlyDictionary<int, ObjectGuid>.Keys => InternallyManagedDictionary.Keys;

		/// <inheritdoc />
		ICollection<ObjectGuid> IDictionary<int, ObjectGuid>.Values => InternallyManagedDictionary.Values;

		/// <inheritdoc />
		ICollection<int> IDictionary<int, ObjectGuid>.Keys => InternallyManagedDictionary.Keys;

		/// <inheritdoc />
		IEnumerable<ObjectGuid> IReadOnlyDictionary<int, ObjectGuid>.Values => InternallyManagedDictionary.Values;
	}
}
