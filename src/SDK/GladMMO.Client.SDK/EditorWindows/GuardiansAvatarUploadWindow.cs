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
	public sealed class GuardiansAvatarUploadWindow : BaseCustomContentUploadEditorWindow
	{
		[SerializeField]
		private UnityEngine.GameObject AvatarPrefab;

		public GuardiansAvatarUploadWindow()
			: base(UserContentType.Avatar)
		{

		}

		[MenuItem("VRGuardians/Content/AvatarUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansAvatarUploadWindow));
		}

		protected override void AuthenticatedOnGUI()
		{
			AvatarPrefab = EditorGUILayout.ObjectField("Avatar Prefab", AvatarPrefab, typeof(GameObject), false) as GameObject;

			if (AvatarPrefab != null)
			{
				if(PrefabUtility.GetPrefabAssetType(AvatarPrefab) == PrefabAssetType.NotAPrefab)
				{
					AvatarPrefab = null;
					Debug.LogError($"Provided avatar prefab MUST be a prefab.");
					return;
				}
			}
			else
				return; //TODO: Try to discover the avatar's prefab in the scene.

			//We need the avatar data definition now.
			AvatarDefinitionData definitionData = AvatarPrefab.GetComponent<AvatarDefinitionData>();

			if (definitionData == null)
				Debug.LogError($"Provided avatar must contain Component: {nameof(AvatarDefinitionData)} on root {nameof(GameObject)}");

			base.OnRenderUploadGUI(definitionData, AvatarPrefab, token =>
			{
				//DO NOT REFERNECE THE ABOVE DEFINITION. IT NO LONGER EXISTS
				//THIS IS DUE TO SCENE RELOAD
				definitionData = AvatarPrefab.GetComponent<AvatarDefinitionData>();

				definitionData.ContentGuid = token.ContentGuid;
				definitionData.ContentId = token.ContentId;

				PrefabUtility.SavePrefabAsset(AvatarPrefab);
			});
		}

		protected override void UnAuthenticatedOnGUI()
		{
			//TODO: Redirect to login.
		}
	}
}