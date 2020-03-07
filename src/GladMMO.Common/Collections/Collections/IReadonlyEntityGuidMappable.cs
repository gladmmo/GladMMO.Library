using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyEntityGuidMappable<TValue> : Glader.Essentials.IReadonlyEntityGuidMappable<ObjectGuid, TValue>, IEnumerable<TValue>
	{
		bool TryGetValue(ObjectGuid key, out TValue value);
	}
}
