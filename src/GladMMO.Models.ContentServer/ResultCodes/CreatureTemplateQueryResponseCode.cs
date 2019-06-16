using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Response code enumeration for the <see cref="CreatureTemplateQueryResponseModel"/>.
	/// </summary>
	public enum CreatureTemplateQueryResponseCode
	{
		Success = 1,

		/// <summary>
		/// Indicates that no templates with the specified id were found.
		/// </summary>
		NoneFound = 2,
	}
}
