using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of all game payload operation codes.
	/// </summary>
	public enum GamePayloadOperationCode : int
	{
		/// <summary>
		/// For the session claim payloads.
		/// </summary>
		ClientSessionClaim = 1,

		EntityVisibilityChange = 2,

		PlayerSelfSpawn = 3,

		MovementDataUpdate = 4,

		ServerTimeSyncronization = 5,

		FieldValueUpdate = 6,

		/// <summary>
		/// Opcode used for telling the client
		/// to load a new scene.
		/// </summary>
		LoadNewScene = 7,

		PlayerRotationUpdate = 8,

		ModelChangeRequest = 9,

		PlayerTrackerDataChange = 10,

		ClickToMove = 11,

		GameObjectInteract = 12,

		/// <summary>
		/// Opcode for client requesting a spellcast.
		/// </summary>
		RequestSpellCast = 13,
	}
}