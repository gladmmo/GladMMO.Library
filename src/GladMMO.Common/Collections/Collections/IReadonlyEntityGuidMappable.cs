using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GladMMO
{
	public interface IReadonlyEntityGuidMappable<TValue> : Glader.Essentials.IReadonlyEntityGuidMappable<ObjectGuid, TValue>, IEnumerable<TValue>
	{
		//TODO: Move this to Glader.Essentials
		ReaderWriterLockSlim SyncObj { get; }

		bool TryGetValue(ObjectGuid key, out TValue value);
	}
}
