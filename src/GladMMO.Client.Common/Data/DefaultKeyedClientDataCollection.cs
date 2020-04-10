using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultKeyedClientDataCollection<TClientDataType> : IKeyedClientDataCollection<TClientDataType>
	{
		private IReadOnlyDictionary<int, TClientDataType> InternalCollection { get; }

		public DefaultKeyedClientDataCollection([NotNull] IReadOnlyDictionary<int, TClientDataType> internalCollection)
		{
			InternalCollection = internalCollection ?? throw new ArgumentNullException(nameof(internalCollection));
		}

		public bool Contains(int key)
		{
			return InternalCollection.ContainsKey(key);
		}

		public IEnumerator<TClientDataType> GetEnumerator()
		{
			return InternalCollection.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count => InternalCollection.Count;

		public TClientDataType this[int index] => InternalCollection[index];
	}
}
