using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerStaticSpawnPointDefinition : StaticSpawnPointDefinition
	{
		public override EntityType EntitySpawnType => EntityType.Player;

		[HideInInspector]
		[SerializeField]
		private int _PlayerSpawnPointId = -1; //default to -1 so it's not known.

		/// <summary>
		/// The ID of the spawnpoint.
		/// </summary>
		public int PlayerSpawnPointId
		{
			get => _PlayerSpawnPointId;
			set => _PlayerSpawnPointId = value; //TODO: Make internal
		}
	}
}
