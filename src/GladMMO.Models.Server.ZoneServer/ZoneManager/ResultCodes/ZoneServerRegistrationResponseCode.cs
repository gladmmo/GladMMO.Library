using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of response codes associated with <see cref="ZoneServerRegistrationResponse"/>
	/// </summary>
	public enum ZoneServerRegistrationResponseCode
	{
		/// <summary>
		/// Indicates the registration was successful.
		/// </summary>
		Success = 1,

		GeneralServerError = 2,

		/// <summary>
		/// Indicates that the zoneserver has already been registered.
		/// It can only be registered once ever (nothing ensures this yet though)
		/// </summary>
		ZoneAlreadyRegistered = 3,

		/// <summary>
		/// Indicates the world id the zone server is attempting to use
		/// does not exist.
		/// </summary>
		WorldRequestedNotFound = 4,
	}
}
