using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class EnsureCrossSceneSingletonsLoaded : MonoBehaviour
	{
		public static HashSet<CrossSceneObjectType> LoadedTypes { get; } = new HashSet<CrossSceneObjectType>();

		[SerializeField]
		private  GameObject[] CrossScenePrefabs = new GameObject[0];

		private void Awake()
		{
			foreach (var prefab in CrossScenePrefabs)
			{
				CrossSceneLivingObject crossSceneDefinition = prefab.GetComponent<CrossSceneLivingObject>();

				if(crossSceneDefinition == null)
					throw new InvalidOperationException($"Tried to create GameObject: {prefab.name} as CrossScene singleton but doesn't have {nameof(CrossSceneLivingObject)} component attached.");

				//Don't create multiple of this.
				if (LoadedTypes.Contains(crossSceneDefinition.Type))
					continue;

				Instantiate(prefab);
				LoadedTypes.Add(crossSceneDefinition.Type);
			}
		}
	}
}
