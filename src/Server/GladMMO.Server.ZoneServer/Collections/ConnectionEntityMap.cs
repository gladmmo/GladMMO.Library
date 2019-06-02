using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyConnectionEntityCollection : IReadOnlyDictionary<int, NetworkEntityGuid>
	{

	}

	public interface IConnectionEntityCollection : IDictionary<int, NetworkEntityGuid>
	{

	}

	public sealed class ConnectionEntityMap : IReadonlyConnectionEntityCollection, IConnectionEntityCollection
	{
		private Dictionary<int, NetworkEntityGuid> InternallyManagedDictionary { get; }

		public ConnectionEntityMap()
		{
			InternallyManagedDictionary = new Dictionary<int, NetworkEntityGuid>();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<int, NetworkEntityGuid>> GetEnumerator()
		{
			return InternallyManagedDictionary.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)InternallyManagedDictionary).GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(KeyValuePair<int, NetworkEntityGuid> item)
		{
			InternallyManagedDictionary.Add(item.Key, item.Value);
		}

		/// <inheritdoc />
		public void Clear()
		{
			InternallyManagedDictionary.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<int, NetworkEntityGuid> item)
		{
			return InternallyManagedDictionary.ContainsKey(item.Key);
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<int, NetworkEntityGuid>[] array, int arrayIndex)
		{
			throw new NotImplementedException($"TODO: Implement copy.");
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<int, NetworkEntityGuid> item)
		{
			return InternallyManagedDictionary.Remove(item.Key);
		}

		/// <inheritdoc />
		int ICollection<KeyValuePair<int, NetworkEntityGuid>>.Count => InternallyManagedDictionary.Count;

		/// <inheritdoc />
		public bool IsReadOnly => false;

		/// <inheritdoc />
		int IReadOnlyCollection<KeyValuePair<int, NetworkEntityGuid>>.Count => InternallyManagedDictionary.Count;

		/// <inheritdoc />
		public void Add(int key, NetworkEntityGuid value)
		{
			InternallyManagedDictionary.Add(key, value);
		}

		/// <inheritdoc />
		bool IDictionary<int, NetworkEntityGuid>.ContainsKey(int key)
		{
			return InternallyManagedDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool Remove(int key)
		{
			return InternallyManagedDictionary.Remove(key);
		}

		/// <inheritdoc />
		bool IDictionary<int, NetworkEntityGuid>.TryGetValue(int key, out NetworkEntityGuid value)
		{
			return InternallyManagedDictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		bool IReadOnlyDictionary<int, NetworkEntityGuid>.ContainsKey(int key)
		{
			return InternallyManagedDictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		bool IReadOnlyDictionary<int, NetworkEntityGuid>.TryGetValue(int key, out NetworkEntityGuid value)
		{
			return InternallyManagedDictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public NetworkEntityGuid this[int key]
		{
			get => InternallyManagedDictionary[key];
			set => InternallyManagedDictionary[key] = value;
		}

		/// <inheritdoc />
		IEnumerable<int> IReadOnlyDictionary<int, NetworkEntityGuid>.Keys => InternallyManagedDictionary.Keys;

		/// <inheritdoc />
		ICollection<NetworkEntityGuid> IDictionary<int, NetworkEntityGuid>.Values => InternallyManagedDictionary.Values;

		/// <inheritdoc />
		ICollection<int> IDictionary<int, NetworkEntityGuid>.Keys => InternallyManagedDictionary.Keys;

		/// <inheritdoc />
		IEnumerable<NetworkEntityGuid> IReadOnlyDictionary<int, NetworkEntityGuid>.Values => InternallyManagedDictionary.Values;
	}
}
