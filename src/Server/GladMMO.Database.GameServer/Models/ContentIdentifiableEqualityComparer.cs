using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class ContentIdentifiableEqualityComparer<TContentType> : IEqualityComparer<TContentType>
		where TContentType : IContentIdentifiable
	{
		public bool Equals(TContentType x, TContentType y)
		{
			if(x == null)
				return y == null;
			else if(y == null)
				return false;
			else
				return x.ContentId == y.ContentId;
		}

		public int GetHashCode(TContentType obj)
		{
			if(obj == null)
				return -1;

			return obj.ContentId;
		}
	}
}
