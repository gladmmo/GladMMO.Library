using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using UnityEditor;
using UnityEngine;

namespace Booma.Proxy
{
	//based on: https://unity3d.college/2016/09/12/unity-oninspectorgui/
	[CustomEditor(typeof(StaticSpawnPointDefinition))]
	public class StaticSpawnPointDefinitionEditor : ExtendedMonoBehaviourTypeDrawer
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
}
