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
using Microsoft.Azure.Storage.Blob;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO.SDK
{
	//TODO: Refactor
	public sealed class GuardiansAvatarUploadWindow : AuthenticatableEditorWindow
	{
		[SerializeField]
		private UnityEngine.GameObject AvatarPrefab;

		[SerializeField]
		private string AssetBundlePath;

		[MenuItem("VRGuardians/Content/AvatarUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansAvatarUploadWindow));
		}

		protected override void AuthenticatedOnGUI()
		{
			//TODO: Validate scene file
			AvatarPrefab = EditorGUILayout.ObjectField("Avatar Prefab", AvatarPrefab, typeof(GameObject), false) as GameObject;

			if(AvatarPrefab != null)
				if(PrefabUtility.GetPrefabAssetType(AvatarPrefab) == PrefabAssetType.NotAPrefab)
				{
					AvatarPrefab = null;
					Debug.LogError($"Provided avatar prefab MUST be a prefab.");
				}

			if(GUILayout.Button("Build Avatar AssetBundle"))
			{
				//Once authenticated we need to try to build the bundle.
				ProjectVindictiveAssetbundleBuilder builder = new ProjectVindictiveAssetbundleBuilder(AvatarPrefab);

				//TODO: Handle uploading build
				AssetBundleManifest manifest = builder.BuildBundle();

				//TODO: Refactor all this crap
				AssetBundlePath = manifest.GetAllAssetBundles().First();

				Debug.Log($"Generated AssetBundle with Path: {AssetBundlePath}");

				return;
			}

			if(GUILayout.Button("Upload Assetbundle"))
			{
				IDownloadableContentServerServiceClient ucmService =
					new DownloadableContentServiceClientFactory()
						.Create(EmptyFactoryContext.Instance);

				//Done out here, must be called on the main thread
				string projectPath = Application.dataPath.ToLower().TrimEnd(@"assets".ToCharArray());

				Thread uploadThread = new Thread(new ThreadStart(async () =>
				{
					try
					{
						ResponseModel<ContentUploadToken, ContentUploadResponseCode> avatarUploadToken = await ucmService.GetNewAvatarUploadUrl();

						//TODO: Better handling.
						if(!avatarUploadToken.isSuccessful)
							throw new Exception($"AVATAR FAILED.");

						string uploadUrl = avatarUploadToken.Result.UploadUrl;
						Debug.Log($"Uploading to: {uploadUrl}.");
						var cloudBlockBlob = new CloudBlockBlob(new Uri(uploadUrl));
						await cloudBlockBlob.UploadFromFileAsync(Path.Combine(projectPath, "AssetBundles", "temp", AssetBundlePath));
					}
					catch (Exception e)
					{
						Debug.LogError($"Failed to upload Avatar. Error: {e.Message}\n\nStack: {e.StackTrace}");
						throw;
					}
				}));

				uploadThread.Start();
			}
		}

		protected override void UnAuthenticatedOnGUI()
		{
			//TODO: Redirect to login.
		}
	}
}