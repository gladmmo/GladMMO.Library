using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class WorldInstanceableEntryModelCreationContext
	{
		public long WorldId { get; }

		public WorldInstanceableEntryModelCreationContext(long worldId)
		{
			if (worldId < 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			WorldId = worldId;
		}
	}
}
