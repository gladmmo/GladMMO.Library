using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DatabaseModelKeyableEquailityComparer<TModelType> : EqualityComparer<TModelType>
		where TModelType : IDatabaseModelKeyable
	{
		public static DatabaseModelKeyableEquailityComparer<TModelType> Instance { get; } = new DatabaseModelKeyableEquailityComparer<TModelType>();

		public override bool Equals(TModelType x, TModelType y)
		{
			if (x == null)
				return y == null;
			else if (y == null)
				return false;
			else
				return x.EntryKey == y.EntryKey;
		}

		public override int GetHashCode(TModelType obj)
		{
			if (obj == null)
				return 0;

			return obj.EntryKey.GetHashCode();
		}
	}
}
