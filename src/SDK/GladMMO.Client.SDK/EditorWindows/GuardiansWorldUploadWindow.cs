using System; using FreecraftCore;
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
using Refit;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO.SDK
{
	public sealed class GuardiansWorldUploadWindow : BaseCustomContentUploadEditorWindow
	{
		[SerializeField]
		private UnityEngine.Object SceneObject;

		public GuardiansWorldUploadWindow()
			: base(UserContentType.World)
		{

		}

		[MenuItem("VRGuardians/Content/WorldUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansWorldUploadWindow));
		}

		protected override void AuthenticatedOnGUI()
		{
			SceneObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneManager.GetActiveScene().path);

			//TODO: Validate scene file
			EditorGUILayout.ObjectField("Scene", SceneObject, typeof(SceneAsset), false);

			//If there is a world definition in the scene we should utilize it.
			//TODO: We should do more than just render it.
			WorldDefinitionData definitionData = FindObjectOfType<WorldDefinitionData>();

			//Let's create a definition if one doesn't already exist.
			if (definitionData == null)
				definitionData = CreateWorldDefinitionData();

			base.OnRenderUploadGUI(definitionData, SceneObject, token =>
			{
				//DO NOT REFERNECE THE ABOVE DEFINITION. IT NO LONGER EXISTS
				//THIS IS DUE TO SCENE RELOAD
				definitionData = FindObjectOfType<WorldDefinitionData>();

				definitionData.ContentGuid = token.ContentGuid;
				definitionData.ContentId = token.ContentId;

				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
				EditorUtility.SetDirty(definitionData);
			});
		}

		private static WorldDefinitionData CreateWorldDefinitionData()
		{
			WorldDefinitionData definitionData;
			definitionData = new GameObject("World Definition").AddComponent<WorldDefinitionData>();

			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			EditorUtility.SetDirty(definitionData);

			return definitionData;
		}

		protected override void UnAuthenticatedOnGUI()
		{
			//TODO: Redirect to login.
		}
	}
}