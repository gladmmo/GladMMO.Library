using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public sealed class TextToolWindow : EditorWindow
	{
		[MenuItem("Tools/Fix Non-Dynamic Texts")]
		public static void ShowWindow()
		{
			foreach (var text in EditorSceneManager.GetAllScenes()
				.Where(s => s.isLoaded)
				.SelectMany(s => s.GetRootGameObjects())
				.SelectMany(go => go.GetComponentsInChildren<UnityEngine.UI.Text>(true)))
			{
				if (text.font != null && !text.font.dynamic)
				{
					text.fontStyle = FontStyle.Normal;

					EditorUtility.SetDirty(text);
					EditorSceneManager.MarkSceneDirty(text.gameObject.scene);
				}
			}

			if (Selection.activeGameObject != null)
			{
				foreach(var text in Selection.activeGameObject
					.GetComponentsInChildren<UnityEngine.UI.Text>(true))
				{
					if(text.font != null && !text.font.dynamic)
					{
						text.fontStyle = FontStyle.Normal;

						EditorUtility.SetDirty(text);
						EditorSceneManager.MarkSceneDirty(text.gameObject.scene);
					}
				}
			}
		}
	}
}
