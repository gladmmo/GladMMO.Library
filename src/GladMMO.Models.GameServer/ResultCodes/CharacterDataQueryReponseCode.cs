using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum CharacterDataQueryReponseCode
	{
		/// <summary>
		/// Indicates the creation was successful.
		/// </summary>
		Success = 1,

		GeneralServerError = 2,

		CharacterNotFound = 3,
	}
}
