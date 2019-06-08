using System;
using System.Collections.Generic;
using System.Text;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	//TODO: If we fail to download the world we probably don't want to actually additively load, so we need a good way to destroy this
	public sealed class DelayedZoneGameSceneLoader : MonoBehaviour
	{
		void Awake()
		{
			//When this component is first encountered, it is not ready.
			//We need it to survive into the next scene to load the actual zone client stuff.
			DontDestroyOnLoad(this);
		}

		void Start()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			//We don't want to do this again
			SceneManager.sceneLoaded -= OnSceneLoaded;

			//We have to load the zone scene, which contains the networking client and stuff.
			//We must load it additively so it combines with the asset bundle loaded scene.
			//SceneController.LoadLevel((int)GameInitializableSceneSpecificationAttribute.SceneType.ZoneGameScene, LoadSceneMode.Additive);
			//We can't use the scene service dependency since this must survive between scenes.
			SceneManager.LoadSceneAsync((int)GameSceneType.InstanceServerScene, LoadSceneMode.Additive).allowSceneActivation = true;
			Destroy(this);
		}
	}
}
