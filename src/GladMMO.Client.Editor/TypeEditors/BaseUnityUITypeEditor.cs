using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Fasterflect;
using Glader.Essentials;
using UnityEditor;

namespace GladMMO
{
	[CustomEditor(typeof(BaseUnityUI), true)]
	public class BaseUnityUITypeEditor : Editor
	{
		/// <inheritdoc />
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var t = (BaseUnityUI)target;

			//HelloKitty: At the bottom we'll just add the enum popup/dropdown for easy selection.
			t.SetFieldValue("_RegisterationKey", Convert.ToInt32(EditorGUILayout.EnumPopup((UnityUIRegisterationKey)t.RegisterationKey)));
		}
	}

	[CustomEditor(typeof(UnitFrameElementsAdapter), true)]
	public sealed class UnitFrameElementsAdapterTypeEditor : Editor
	{
		/// <inheritdoc />
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var t = (UnitFrameElementsAdapter)target;

			//HelloKitty: At the bottom we'll just add the enum popup/dropdown for easy selection.
			t.SetFieldValue("_RegisterationKey", Convert.ToInt32(EditorGUILayout.EnumPopup((UnityUIRegisterationKey)t.RegisterationKey)));
		}
	}
}
