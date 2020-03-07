﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//From: https://github.com/BoomaNation/Booma.Library/blob/c5af8ed85e0ddd07f86f941588d8962824ae04be/src/Booma.Instance.Common/Entity/Spawn/ISpawnPointStrategy.cs
	public interface ISpawnPointStrategy
	{
		/// <summary>
		/// The spawn type of the strategy.
		/// Could be player or creature.
		/// </summary>
		EntityTypeId EntitySpawnType { get; }

		/// <summary>
		/// Generates a spawnpoint.
		/// </summary>
		/// <returns>A non-null Transform.</returns>
		SpawnPointData GetSpawnPoint();
	}
}