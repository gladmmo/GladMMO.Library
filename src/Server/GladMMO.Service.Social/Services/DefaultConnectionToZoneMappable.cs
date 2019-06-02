using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class DefaultConnectionToZoneMappable : IConnectionToZoneMappable
	{
		private Dictionary<string, int> InternalMap { get; }

		public DefaultConnectionToZoneMappable()
		{
			InternalMap = new Dictionary<string, int>();
		}

		/// <inheritdoc />
		public void Register(string key, int value)
		{
			InternalMap.Add(key, value);
		}

		/// <inheritdoc />
		public bool Contains(string key)
		{
			return InternalMap.ContainsKey(key);
		}

		/// <inheritdoc />
		public int Retrieve(string key)
		{
			return InternalMap[key];
		}

		/// <inheritdoc />
		public bool Unregister(string key)
		{
			return InternalMap.Remove(key);
		}
	}
}
