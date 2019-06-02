using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Represents a work queue entry 
	/// </summary>
	public sealed class ZoneInstanceWorkEntry
	{
		/// <summary>
		/// The requested world id of the instance.
		/// </summary>
		public long WorldId { get; }

		//TODO: many more details can be added here, like instance/room settings.

		/// <inheritdoc />
		public ZoneInstanceWorkEntry(long worldId)
		{
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			WorldId = worldId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ZoneInstanceWorkEntry()
		{
			
		}
	}
}
