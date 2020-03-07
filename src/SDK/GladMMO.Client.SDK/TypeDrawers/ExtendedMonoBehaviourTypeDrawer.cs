using System; using FreecraftCore;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Fasterflect;
using UnityEditor;
using UnityEngine;

namespace GladMMO
{
	[CustomEditor(typeof(GladMMOSDKMonoBehaviour), true)]
	public class ExtendedMonoBehaviourTypeDrawer : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			//Foreach method with a ButtonAttribute attached to it we should draw
			//a button and call the method if it's invoked.
			foreach (MethodInfo mi in target.GetType().MethodsWith(Flags.InstanceAnyVisibility, typeof(ButtonAttribute)))
			{
				if (GUILayout.Button(AddSpacesToSentence(mi.Name, true)))
				{
					//TODO: Is it faster to invoke from the MethodInfo or from Fasterflects CallMethod extension?
					target.CallMethod(mi.Name);
				}
			}
		}

		//From: https://stackoverflow.com/a/272929
		public static string AddSpacesToSentence(string text, bool preserveAcronyms)
		{
			if(string.IsNullOrWhiteSpace(text))
				return string.Empty;
			StringBuilder newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);
			for(int i = 1; i < text.Length; i++)
			{
				if(char.IsUpper(text[i]))
					if((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) || (preserveAcronyms && char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1])))
						newText.Append(' ');
				newText.Append(text[i]);
			}
			return newText.ToString();
		}
	}
}
