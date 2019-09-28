using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class CreatureStaticSpawnPointDefinition : StaticSpawnPointDefinition
	{
		public override EntityType EntitySpawnType => EntityType.Creature;

		[HideInInspector]
		//[Tooltip("This should be the ID of the creature template that holds information about what creature is suppose to spawn.")]
		[SerializeField]
		private int _CreatureTemplateId = -1; //default to -1 so it's not known.

		//We don't show this in the inspector because the user should not be changing this.
		[HideInInspector]
		[SerializeField]
		private int _CreatureInstanceId = -1; //default to -1 so it's not known.

		/// <summary>
		/// The ID of the creature template to use for spawning.
		/// </summary>
		public int CreatureTemplateId
		{
			get => _CreatureTemplateId;
			set => _CreatureTemplateId = value; //TODO: Make internal
		}

		/// <summary>
		/// The ID of the creature instance. Basically the global instance id of a creature.
		/// </summary>
		public int CreatureInstanceId
		{
			get => _CreatureInstanceId;
			set => _CreatureInstanceId = value; //TODO: Make internal
		}
	}
}
