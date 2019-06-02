using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Collection of sessions connected.
	/// </summary>
	public sealed class DefaultSessionCollection : ISessionCollection
	{
		/// <inheritdoc />
		public int Count => ZoneMap.Count;

		private ConcurrentDictionary<int, ZoneClientSession> ZoneMap { get; } = new ConcurrentDictionary<int, ZoneClientSession>();

		public DefaultSessionCollection()
		{
			
		}

		/// <inheritdoc />
		IEnumerator<ZoneClientSession> IEnumerable<ZoneClientSession>.GetEnumerator()
		{
			return ZoneMap.Values.GetEnumerator();
		}

		/// <inheritdoc />
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)ZoneMap).GetEnumerator();
		}

		/// <inheritdoc />
		public void Register(int key, ZoneClientSession value)
		{
			if(!ZoneMap.TryAdd(key, value))
				throw new InvalidOperationException($"Failed to add Key: {key} Type: {nameof(ZoneClientSession)} to {nameof(DefaultSessionCollection)}");
		}

		/// <inheritdoc />
		public bool Contains(int key)
		{
			return ZoneMap.ContainsKey(key);
		}

		/// <inheritdoc />
		public ZoneClientSession Retrieve(int key)
		{
			//Callers should check if it contains the key
			//or this method should expect to throw.
			return ZoneMap[key];
		}

		/// <inheritdoc />
		public bool Unregister(int key)
		{
			return ZoneMap.TryRemove(key, out ZoneClientSession z);
		}
	}
}
