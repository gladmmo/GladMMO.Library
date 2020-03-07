using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all the flags set on a gameserver.
	/// </summary>
	[Flags]
	public enum GameServerStatusFlags
	{
		/// <summary>
		/// Indicates unknown flags.
		/// </summary>
		Unknown = 1 << 0,

		/// <summary>
		/// The Online flag.
		/// Check if this is not set to determine if offline.
		/// </summary>
		Online = 1 << 1,

		NewServer = 1 << 3,

		//TODO: We can add more flags here like PvP/Hardcore/Seasonal or something.
	}
}
