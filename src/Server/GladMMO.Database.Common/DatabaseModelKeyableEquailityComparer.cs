using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DatabaseModelKeyableEquailityComparer : EqualityComparer<IDatabaseModelKeyable>
	{
		public override bool Equals(IDatabaseModelKeyable x, IDatabaseModelKeyable y)
		{
			if (x == null)
				return y == null;
			else if (y == null)
				return false;
			else
				return x.EntryKey == y.EntryKey;
		}

		public override int GetHashCode(IDatabaseModelKeyable obj)
		{
			if (obj == null)
				return 0;

			return obj.EntryKey.GetHashCode();
		}
	}
}
