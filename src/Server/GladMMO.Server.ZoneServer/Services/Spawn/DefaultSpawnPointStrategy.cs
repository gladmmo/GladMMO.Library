using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// The default spawn point provider.
	/// </summary>
	public sealed class DefaultSpawnPointStrategy : ISpawnPointStrategy
	{
		public EntitySpawnType EntityType => EntitySpawnType.Player;

		public SpawnPointData GetSpawnPoint()
		{
			return new SpawnPointData(Vector3.zero, Quaternion.identity);
		}
	}
}
