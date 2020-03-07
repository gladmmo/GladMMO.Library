using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Bitmask for network movement tracker type.
	/// </summary>
	[Flags]
	public enum NetworkMovementTrackerTypeFlags : int
	{
		None = 0,

		Head = 1 << 0,
		RightHand = 1 << 1,
		LeftHand = 1 << 2
	}
}
