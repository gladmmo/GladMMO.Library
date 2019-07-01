using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyEntityGuidMappable<TValue> : Glader.Essentials.IReadonlyEntityGuidMappable<NetworkEntityGuid, TValue>
	{

	}
}
