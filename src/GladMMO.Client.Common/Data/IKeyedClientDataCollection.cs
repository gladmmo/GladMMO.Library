using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IKeyedClientDataCollection<out TClientDataType> : IReadOnlyList<TClientDataType>, IReadOnlyCollection<TClientDataType>
	{
		bool Contains(int key);
	}
}
