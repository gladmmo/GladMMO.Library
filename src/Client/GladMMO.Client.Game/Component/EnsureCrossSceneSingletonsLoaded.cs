using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public sealed class EnsureCrossSceneSingletonsLoaded : MonoBehaviour
	{
		public static HashSet<CrossSceneObjectType> LoadedTypes { get; } = new HashSet<CrossSceneObjectType>();

		[SerializeField]
		private GameObject[] CrossScenePrefabs = new GameObject[0];

		private void Awake()
		{
			//Loaded shared scene if doesn't exist right now.
			Scene scene = SceneManager.GetSceneByName(GladMMOClientConstants.SHARED_SCENE_NAME);
			if (!scene.isLoaded)
			{
				SceneManager.LoadScene(GladMMOClientConstants.SHARED_SCENE_NAME, LoadSceneMode.Additive);
				scene = SceneManager.GetSceneByName(GladMMOClientConstants.SHARED_SCENE_NAME);
			}

			foreach (var prefab in CrossScenePrefabs)
			{
				CrossSceneLivingObject crossSceneDefinition = prefab.GetComponent<CrossSceneLivingObject>();

				if(crossSceneDefinition == null)
					throw new InvalidOperationException($"Tried to create GameObject: {prefab.name} as CrossScene singleton but doesn't have {nameof(CrossSceneLivingObject)} component attached.");

				//Don't create multiple of this.
				if (LoadedTypes.Contains(crossSceneDefinition.Type))
					continue;

				LoadedTypes.Add(crossSceneDefinition.Type);
				GameObject root = Instantiate(prefab);

				//Move to shared.
				SceneManager.MoveGameObjectToScene(root, scene);
			}
		}
	}
}
