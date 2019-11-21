using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nito.AsyncEx;

namespace GladMMO
{
	public interface IDirtyableMovementDataCollection : ICollectionMapDirtyable<NetworkEntityGuid>, IReadonlyEntityGuidMappable<IMovementData>
	{
		object SyncObject { get; }
	}
}
