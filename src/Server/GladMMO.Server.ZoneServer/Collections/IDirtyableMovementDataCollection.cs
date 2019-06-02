using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IDirtyableMovementDataCollection : ICollectionMapDirtyable<NetworkEntityGuid>, IReadonlyEntityGuidMappable<IMovementData>
	{
		
	}
}
