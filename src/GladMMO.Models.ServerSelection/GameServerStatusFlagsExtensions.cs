using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Extensions for the <see cref="GameServerStatusFlags"/> Type.
	/// </summary>
	public static class GameServerStatusFlagsExtensions
	{
		/// <summary>
		/// Indicates if the server has an offline flag indicator.
		/// </summary>
		/// <param name="flags">The flags.</param>
		/// <returns>True if the flags indicate the gameserver is offline.</returns>
		public static bool IsOffline(this GameServerStatusFlags flags)
		{
			return !flags.HasFlag(GameServerStatusFlags.Online);
		}
	}
}
