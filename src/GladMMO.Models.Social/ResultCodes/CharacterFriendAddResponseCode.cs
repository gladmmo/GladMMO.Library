using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum CharacterFriendAddResponseCode
	{
		/// <summary>
		/// Indicates the request was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Unknown serverside error occurred.
		/// </summary>
		GeneralServerError = 2,

		/// <summary>
		/// Indicates that the player is already friends with that player.
		/// </summary>
		AlreadyFriends = 3,

		/// <summary>
		/// The character is not found.
		/// </summary>
		CharacterNotFound = 4,
	}
}
