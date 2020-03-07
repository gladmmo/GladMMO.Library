using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of friendship relation states.
	/// </summary>
	public enum FriendshipRelationshipState
	{
		/// <summary>
		/// Default state is that a relationship is
		/// pending.
		/// </summary>
		Pending = 0,

		/// <summary>
		/// Indicates that a friend relationship has been
		/// accepted and established. They are friends.
		/// </summary>
		Accepted = 1,

		/// <summary>
		/// Indicates that a relationship has been blocked.
		/// This does not mean that the user has been blocked
		/// but that requests between these users are blocked
		/// meaning that no new requests can be sent until the request
		/// block has been lifted.
		/// </summary>
		Blocked = 2,
	}
}
