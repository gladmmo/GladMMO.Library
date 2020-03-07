using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerStaticSpawnPointDefinition : StaticSpawnPointDefinition
	{
		public override EntityTypeId EntitySpawnType => EntityTypeId.TYPEID_PLAYER;

		[HideInInspector]
		[SerializeField]
		private int _PlayerSpawnPointId = -1; //default to -1 so it's not known.

		[HideInInspector]
		[SerializeField]
		private bool _isInstanceReserved = false;

		/// <summary>
		/// The ID of the spawnpoint.
		/// </summary>
		public int PlayerSpawnPointId
		{
			get => _PlayerSpawnPointId;
			set => _PlayerSpawnPointId = value; //TODO: Make internal
		}

		public bool isInstanceReserved
		{
			get => _isInstanceReserved;
			set => _isInstanceReserved = value;
		}
	}
}
