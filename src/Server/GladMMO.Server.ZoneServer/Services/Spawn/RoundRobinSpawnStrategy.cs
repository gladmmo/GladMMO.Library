using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class RoundRobinSpawnStrategy : ISpawnPointStrategy
	{
		private PlayerSpawnStrategyQueue SpawnStrategies { get; }

		private readonly object SyncObj = new object();

		public RoundRobinSpawnStrategy([NotNull] PlayerSpawnStrategyQueue spawnStrategies)
		{
			SpawnStrategies = spawnStrategies ?? throw new ArgumentNullException(nameof(spawnStrategies));
		}

		public SpawnPointData GetSpawnPoint()
		{
			//Even though the above is a concurrent queue we need to lock
			//because the queue could be in an empty state in the middle of a spawn point query
			//and we cannot provide valid data in that case.
			lock (SyncObj)
			{
				//We should just default here, since we have no valid data.
				if(SpawnStrategies.Count == 0 || !SpawnStrategies.TryDequeue(out var pointStrategy))
					throw new InvalidOperationException($"Failed to obtain an {nameof(ISpawnPointStrategy)} from {nameof(SpawnStrategies)}.");

				SpawnPointData data = pointStrategy.GetSpawnPoint();
				SpawnStrategies.Enqueue(pointStrategy);

				return data;
			}
		}
	}
}
