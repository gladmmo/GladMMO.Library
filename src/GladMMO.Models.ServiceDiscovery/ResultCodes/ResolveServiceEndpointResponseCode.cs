using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all service endpoint response results.
	/// </summary>
	public enum ResolveServiceEndpointResponseCode
	{
		/// <summary>
		/// Indicates that the request was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that the service is unknown or unlisted by the directory.
		/// </summary>
		ServiceUnlisted = 2,

		/// <summary>
		/// Indicates that the service is not available right now.
		/// </summary>
		ServiceUnavailable = 3,

		/// <summary>
		/// Indicates that a general error has occured with the request.
		/// </summary>
		GeneralRequestError = 4
	}
}