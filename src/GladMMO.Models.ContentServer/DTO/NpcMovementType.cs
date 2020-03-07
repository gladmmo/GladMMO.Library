using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all movement types for NPCs.
	/// </summary>
	public enum NpcMovementType : byte
	{
		/// <summary>
		/// Indicates that the NPC does not move from its initial position.
		/// </summary>
		Stationary = 0,

		/// <summary>
		/// Indicates that the NPC follows waypoints from start to end.
		/// </summary>
		WapointBased = 1
	}
}
