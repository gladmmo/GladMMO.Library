using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of entity prefab IDs.
	/// (This is not models/meshes but the actualt prefab objects themselves).
	/// </summary>
	public enum EntityPrefab : int
	{
		Unknown = 0,

		LocalPlayer = 1,

		RemotePlayer = 2,

		NetworkNpc = 3,

		NetworkGameObject = 4,

		MessageBoxText = 5,

		CharacterFriendSlot = 6,

		CharacterGuildSlot = 7,

		NetworkCorpse = 8
	}
}
