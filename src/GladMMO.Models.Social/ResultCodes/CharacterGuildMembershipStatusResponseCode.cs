using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum CharacterGuildMembershipStatusResponseCode
	{
		/// <summary>
		/// Indicates the creation was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that the character is not in a guild.
		/// </summary>
		NoGuild = 2,

		/// <summary>
		/// Indicates an unknown server error.
		/// </summary>
		GeneralServerError = 3,
	}
}
