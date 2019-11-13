using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultKnownSpellSet : IKnownSpellSet
	{
		private readonly object SyncObj = new object();

		private HashSet<int> KnownSpellIdSet { get; } = new HashSet<int>();

		//Not threadsafe
		public IEnumerator<int> GetEnumerator()
		{
			return KnownSpellIdSet.GetEnumerator();
		}

		//Not threadsafe
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Contains(int spellId)
		{
			lock (SyncObj)
				return KnownSpellIdSet.Contains(spellId);
		}

		public bool Add(int spellId)
		{
			lock(SyncObj)
				return KnownSpellIdSet.Add(spellId);
		}
	}
}
