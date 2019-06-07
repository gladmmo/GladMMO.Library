using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all response for a content download URL.
	/// </summary>
	public enum ContentDownloadURLResponseCode
	{
		/// <summary>
		/// Indicates the request was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that the content does not exist.
		/// </summary>
		NoContentId = 2,

		/// <summary>
		/// Indicates that the service for downloading content is unavailable.
		/// </summary>
		ContentDownloadServiceUnavailable = 3,

		/// <summary>
		/// Indicates that the content download is not authorize for the
		/// requesting user.
		/// </summary>
		AuthorizationFailed = 4
	}
}