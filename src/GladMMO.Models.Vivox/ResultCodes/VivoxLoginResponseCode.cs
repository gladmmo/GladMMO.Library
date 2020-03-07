using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all responses for a vivox login attempt.
	/// </summary>
	public enum VivoxLoginResponseCode
	{
		/// <summary>
		/// Indicates the request was successful.
		/// </summary>
		Success = 1,

		GeneralServerError = 2,

		/// <summary>
		/// Indicates that no session character session is actually active for the account.
		/// </summary>
		NoActiveCharacterSession = 3,

		/// <summary>
		/// This can happen if they attempt to join channels they don't belong in.
		/// </summary>
		ChannelUnavailable = 4,
	}
}
