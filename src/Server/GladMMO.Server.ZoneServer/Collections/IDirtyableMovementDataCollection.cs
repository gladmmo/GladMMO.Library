using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nito.AsyncEx;

namespace GladMMO
{
	public interface IDirtyableMovementDataCollection : ICollectionMapDirtyable<ObjectGuid>, IReadonlyEntityGuidMappable<IMovementData>
	{
		object SyncObject { get; }
	}
}
