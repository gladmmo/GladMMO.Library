using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class CreatureStaticSpawnPointDefinition : StaticSpawnPointDefinition
	{
		public override EntitySpawnType EntityType => EntitySpawnType.Creature;

		[Tooltip("This should be the ID of the creature template that holds information about what creature is suppose to spawn.")]
		[SerializeField]
		private int _CreatureTemplateId = -1; //default to -1 so it's not known.

		/// <summary>
		/// The ID of the creature template to use for spawning.
		/// </summary>
		public int CreatureTemplateId => _CreatureTemplateId;
	}
}
