using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class GameObjectStaticSpawnPointDefinition : StaticSpawnPointDefinition
	{
		public override EntityType EntitySpawnType => EntityType.GameObject;

		[HideInInspector]
		[Tooltip("This should be the ID of the gameobject template that holds information about what gameobject is suppose to spawn.")]
		[SerializeField]
		private int _GameObjectTemplateId = -1; //default to -1 so it's not known.

		//We don't show this in the inspector because the user should not be changing this.
		[HideInInspector]
		[SerializeField]
		private int _GameObjectInstanceId = -1; //default to -1 so it's not known.

		/// <summary>
		/// The ID of the GameObject template to use for spawning.
		/// </summary>
		public int GameObjectTemplateId
		{
			get => _GameObjectTemplateId;
			set => _GameObjectTemplateId = value; //TODO: Make internal
		}

		/// <summary>
		/// The ID of the GameObject instance. Basically the global instance id of a GameObject.
		/// </summary>
		public int GameObjectInstanceId
		{
			get => _GameObjectInstanceId;
			set => _GameObjectInstanceId = value; //TODO: Make internal
		}
	}
}
