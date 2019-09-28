using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using UnityEditor;
using UnityEngine;

namespace GladMMO
{
	//based on: https://unity3d.college/2016/09/12/unity-oninspectorgui/
	[CustomEditor(typeof(StaticSpawnPointDefinition))]
	public class StaticSpawnPointDefinitionEditor : ExtendedMonoBehaviourTypeDrawer
	{
		//This needs to be static, otherwise progress bars could be running while you switch TO another window, and then back
		//and then you could press the buttons again. This must be prevented.
		protected static bool isProgressBarActive = false;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}

		protected void DisplayProgressBar(string title, string info, float progress)
		{
			isProgressBarActive = true;
			EditorUtility.DisplayProgressBar(title, info, progress);
		}

		protected void ClearProgressBar()
		{
			isProgressBarActive = false;
			EditorUtility.ClearProgressBar();
		}
	}
}
