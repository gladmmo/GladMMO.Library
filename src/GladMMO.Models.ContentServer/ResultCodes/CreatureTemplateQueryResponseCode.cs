using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum CreatureTemplateQueryResponseCode
	{
		Success = ModelsCommonConstants.RESPONSE_CODE_SUCCESS_VALUE,

		/// <summary>
		/// Indicates that no templates with the specified id were found.
		/// </summary>
		NoneFound = 2,
	}
}
