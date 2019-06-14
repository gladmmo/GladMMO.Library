using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class RoundRobinSpawnStrategy : ISpawnPointStrategy
	{
		private PlayerSpawnStrategyQueue SpawnStrategies { get; }

		public RoundRobinSpawnStrategy([NotNull] PlayerSpawnStrategyQueue spawnStrategies)
		{
			SpawnStrategies = spawnStrategies ?? throw new ArgumentNullException(nameof(spawnStrategies));
		}

		public SpawnPointData GetSpawnPoint()
		{
			//We should just default here, since we have no valid data.
			if (SpawnStrategies.Count == 0 || !SpawnStrategies.TryDequeue(out var pointStrategy))
				return new SpawnPointData(Vector3.zero, Quaternion.identity);

			SpawnPointData data = pointStrategy.GetSpawnPoint();
			SpawnStrategies.Enqueue(pointStrategy);

			return data;
		}
	}
}
