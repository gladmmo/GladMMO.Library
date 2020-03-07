using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum ZoneWorldConfigurationResponseCode
	{
		/// <summary>
		/// Query successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that there was an unknown general error.
		/// </summary>
		GeneralServerError = 2,

		/// <summary>
		/// Indicates that the requested zone does not exist.
		/// </summary>
		ZoneDoesntExist = 3,

		/// <summary>
		/// Indicates that the client was not authorized to
		/// see zone world configuration.
		/// </summary>
		NotAuthorized = 4,
	}
}
