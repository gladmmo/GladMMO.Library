using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Implements simple value-type style comparision semantics for the <see cref="NetworkEntityGuid"/>
	/// type.
	/// </summary>
	public class NetworkGuidEqualityComparer<TType> : EqualityComparer<TType>
		where TType : NetworkEntityGuid
	{
		/// <summary>
		/// Singleton instance of the <see cref="NetworkGuidEqualityComparer{TType}"/>.
		/// </summary>
		public static NetworkGuidEqualityComparer<TType> Instance { get; } = new NetworkGuidEqualityComparer<TType>();

		//Forces use of singleton
		protected NetworkGuidEqualityComparer()
		{

		}

		public override bool Equals(TType x, TType y)
		{
			if(x == null)
			{
				return y == null;
			}
			else if(y == null)
				return false;

			return x.RawGuidValue == y.RawGuidValue;
		}

		public override int GetHashCode(TType obj)
		{
			if(obj == null) throw new ArgumentNullException(nameof(obj));

			//Just return the ulong hash code.
			return obj.RawGuidValue.GetHashCode();
		}
	}
}