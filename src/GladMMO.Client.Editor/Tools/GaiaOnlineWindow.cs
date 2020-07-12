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
	public sealed class GaiaOnlineWindow : EditorWindow
	{
		Material mat;

		[MenuItem("Tools/Make GaiaOnline Avatar Material")]
		public static void ShowWindow()
		{
			GetWindow<GaiaOnlineWindow>("Material Editor Test");
		}

		void OnGUI()
		{
			mat = EditorGUILayout.ObjectField(mat, typeof(Material), false) as Material;

			if (GUILayout.Button("Change") && mat != null)
			{
				mat.SetFloat("_Mode", 2);
				mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				mat.SetInt("_ZWrite", 0);
				mat.DisableKeyword("_ALPHATEST_ON");
				mat.EnableKeyword("_ALPHABLEND_ON");
				mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				mat.renderQueue = 3000;
			}
		}
	}
}
