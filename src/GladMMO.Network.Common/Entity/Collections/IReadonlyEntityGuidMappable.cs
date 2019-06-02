using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyEntityGuidMappable<TValue> : IReadOnlyDictionary<NetworkEntityGuid, TValue>
	{
		
	}
}
