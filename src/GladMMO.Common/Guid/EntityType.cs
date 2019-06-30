using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all Entity types.
	/// </summary>
	public enum EntityType : byte
	{
		/// <summary>
		/// Represents a typeless entity.
		/// </summary>
		None = 0,

		/// <summary>
		/// Player entity.
		/// </summary>
		Player = 1,

		/// <summary>
		/// GameObject entity.
		/// (Such as a Door or a Button)
		/// </summary>
		GameObject = 2,

		Creature = 3,
	}
}