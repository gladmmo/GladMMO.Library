using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GladMMO
{
	public class FixScaleWindow : EditorWindow
	{
		private float CurrentScaleValue = 1.0f;

		[MenuItem("Tools/Fix Scale")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(FixScaleWindow));
		}

		void OnGUI()
		{
			CurrentScaleValue = EditorGUILayout.FloatField("Scale Amount", CurrentScaleValue);

			if (GUILayout.Button("Scale"))
			{
				foreach (Light light in EditorSceneManager.GetActiveScene()
					.GetRootGameObjects()
					.SelectMany(go => go.GetComponentsInChildren<Light>())
					.Where(c => c != null))
				{
					switch (light.type)
					{
						case LightType.Spot:
							break;
						case LightType.Point:
							light.range *= CurrentScaleValue;
							light.intensity *= CurrentScaleValue;
							break;
						case LightType.Area:
							light.areaSize = light.areaSize * CurrentScaleValue;
							light.range *= CurrentScaleValue;
							light.intensity *= CurrentScaleValue;
							break;
						case LightType.Disc:
							break;
						default:
							Debug.Log($"Skipped Light: {light.name} because of Type: {light.type}");
							continue;
					}
				}

				foreach(var probe in EditorSceneManager.GetActiveScene()
					.GetRootGameObjects()
					.SelectMany(go => go.GetComponentsInChildren<ReflectionProbe>())
					.Where(c => c != null))
				{
					probe.size *= CurrentScaleValue;
					probe.blendDistance *= CurrentScaleValue;
				}
			}
		}
	}
}
