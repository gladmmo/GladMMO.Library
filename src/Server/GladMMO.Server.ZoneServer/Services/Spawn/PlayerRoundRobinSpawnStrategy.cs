using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerRoundRobinSpawnStrategy : ISpawnPointStrategy
	{
		private PlayerSpawnPointQueue SpawnPoints { get; }

		private readonly object SyncObj = new object();

		public PlayerRoundRobinSpawnStrategy([NotNull] PlayerSpawnPointQueue spawnPoints)
		{
			SpawnPoints = spawnPoints ?? throw new ArgumentNullException(nameof(spawnPoints));
		}

		public EntityType EntitySpawnType => EntityType.Player;

		public SpawnPointData GetSpawnPoint()
		{
			//Even though the above is a concurrent queue we need to lock
			//because the queue could be in an empty state in the middle of a spawn point query
			//and we cannot provide valid data in that case.
			lock (SyncObj)
			{
				//We should just default here, since we have no valid data.
				if(SpawnPoints.Count == 0 || !SpawnPoints.TryDequeue(out var point))
					throw new InvalidOperationException($"Failed to obtain an {nameof(ISpawnPointStrategy)} from {nameof(SpawnPoints)}.");

				SpawnPoints.Enqueue(point);

				return new SpawnPointData(point.InitialPosition, Quaternion.Euler(0, point.YAxisRotation, 0));
			}
		}
	}
}
