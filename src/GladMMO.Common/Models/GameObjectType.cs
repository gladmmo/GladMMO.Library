using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all known game object types.
	/// </summary>
	public enum GameObjectType
	{
		/// <summary>
		/// Represents just a visual gameobject
		/// with no other functionality.
		/// </summary>
		Visual = 0,

		/// <summary>
		/// Represents a gameobject that teleports
		/// players between worlds.
		/// </summary>
		WorldTeleporter = 1,

		/// <summary>
		/// Represents a gamobject that morphs
		/// a player into a specific avatar model id.
		/// </summary>
		AvatarPedestal = 2,
	}
}
