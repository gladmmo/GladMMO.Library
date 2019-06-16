using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Response code Enumeration for all scene content query responses.
	/// </summary>
	public enum SceneContentQueryResponseCode
	{
		Success = ModelsCommonConstants.RESPONSE_CODE_SUCCESS_VALUE,

		/// <summary>
		/// Indicates that the scene content template is unknown.
		/// </summary>
		TemplateUnknown = 2,

		/// <summary>
		/// Indicates that the caller is unauthorized to access information about the content.
		/// </summary>
		Unauthorized = 3,

		/// <summary>
		/// Indicates an unknown/general error occuring.
		/// </summary>
		GeneralServerError = 4,
	}
}
