using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace GladMMO
{
	public sealed class DebugWindow : EditorWindow
	{
		[MenuItem("Debug/Teleport To Scene Camera")]
		public static void ShowWindow()
		{
			Vector3 position = SceneView
				.GetAllSceneCameras()
				.First().transform.position;

			GameObject playerObject = FindObjectsOfType<GameObject>()
				.FirstOrDefault(go => go.name.ToLower().Contains("localplayer"));

			playerObject.transform.position = position;
		}
	}
}
