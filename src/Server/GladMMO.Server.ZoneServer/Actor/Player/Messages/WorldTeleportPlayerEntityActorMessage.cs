using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class WorldTeleportPlayerEntityActorMessage : EntityActorMessage
	{
		/// <summary>
		/// The spawnpoint of the ID the player should end up at after teleporting.
		/// </summary>
		public int WorldSpawnPointId { get; }

		/// <summary>
		/// The the world teleporter's game object id.
		/// </summary>
		public int WorldTeleportGameObjectId { get; }

		public WorldTeleportPlayerEntityActorMessage(int worldSpawnPointId, int worldTeleportGameObjectId)
		{
			if (worldSpawnPointId <= 0) throw new ArgumentOutOfRangeException(nameof(worldSpawnPointId));
			if (worldTeleportGameObjectId <= 0) throw new ArgumentOutOfRangeException(nameof(worldTeleportGameObjectId));

			WorldSpawnPointId = worldSpawnPointId;
			WorldTeleportGameObjectId = worldTeleportGameObjectId;
		}
	}
}
