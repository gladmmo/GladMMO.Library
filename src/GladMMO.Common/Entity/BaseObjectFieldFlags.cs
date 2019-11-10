using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration for the <see cref="BaseObjectField"/>.UNIT_FIELD_FLAGS
	/// </summary>
	[Flags]
	public enum BaseObjectFieldFlags : int
	{
		/// <summary>
		/// Indicates no flags are set.
		/// </summary>
		None = 0,

		/// <summary>
		/// Indicates that the object cannot be target/selected by the client.
		/// </summary>
		UNIT_FLAG_NOT_SELECTABLE = 1 << 0,
	}
}
