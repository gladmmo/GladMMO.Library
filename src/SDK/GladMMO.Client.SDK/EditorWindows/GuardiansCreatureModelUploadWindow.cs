using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.Azure.Storage.Blob;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO.SDK
{
	//TODO: Refactor
	public sealed class GuardiansCreatureModelUploadWindow : BaseCustomContentUploadEditorWindow
	{
		[SerializeField]
		private UnityEngine.GameObject CreaturePrefab;

		public GuardiansCreatureModelUploadWindow()
			: base(UserContentType.Creature)
		{

		}

		[MenuItem("VRGuardians/Content/CreatureModelUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansCreatureModelUploadWindow));
		}

		protected override void AuthenticatedOnGUI()
		{
			CreaturePrefab = EditorGUILayout.ObjectField("Creature Prefab", CreaturePrefab, typeof(GameObject), false) as GameObject;

			if(CreaturePrefab != null)
			{
				if(PrefabUtility.GetPrefabAssetType(CreaturePrefab) == PrefabAssetType.NotAPrefab)
				{
					CreaturePrefab = null;
					Debug.LogError($"Provided creature prefab MUST be a prefab.");
					return;
				}
			}
			else
				return; //TODO: Try to discover the avatar's prefab in the scene.

			//We need the avatar data definition now.
			CreatureDefinitionData definitionData = CreaturePrefab.GetComponent<CreatureDefinitionData>();

			if(definitionData == null)
				Debug.LogError($"Provided creature must contain Component: {nameof(CreatureDefinitionData)} on root {nameof(GameObject)}");

			base.OnRenderUploadGUI(definitionData, CreaturePrefab, token =>
			{
				//DO NOT REFERNECE THE ABOVE DEFINITION. IT NO LONGER EXISTS
				//THIS IS DUE TO SCENE RELOAD
				definitionData = CreaturePrefab.GetComponent<CreatureDefinitionData>();

				definitionData.ContentGuid = token.ContentGuid;
				definitionData.ContentId = token.ContentId;

				EditorUtility.SetDirty(definitionData);
				EditorUtility.SetDirty(CreaturePrefab);
			});
		}

		protected override void UnAuthenticatedOnGUI()
		{
			//TODO: Redirect to login.
		}
	}
}