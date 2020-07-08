using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum GameContentQueryResponseCode
	{
		/// <summary>
		/// Indicates the request was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates the request's identifier is unknown
		/// and not linked to a known content.
		/// </summary>
		UnknownContentIdentifier = 2,
	}
}
