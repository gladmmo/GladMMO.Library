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

		public WorldTeleportPlayerEntityActorMessage(int worldSpawnPointId)
		{
			if (worldSpawnPointId <= 0) throw new ArgumentOutOfRangeException(nameof(worldSpawnPointId));

			WorldSpawnPointId = worldSpawnPointId;
		}
	}
}
