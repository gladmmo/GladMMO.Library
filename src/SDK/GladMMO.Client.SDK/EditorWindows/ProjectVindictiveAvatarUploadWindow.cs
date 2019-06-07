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
	public sealed class ProjectVindictiveAvatarUploadWindow : AuthenticatableEditorWindow
	{
		[SerializeField]
		private UnityEngine.GameObject AvatarPrefab;

		[SerializeField]
		private string AssetBundlePath;

		[MenuItem("ProjectVindictive/AvatarUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(ProjectVindictiveAvatarUploadWindow));
		}

		protected override void OnGUI()
		{
			base.OnGUI();

			//TODO: Validate scene file
			AvatarPrefab = EditorGUILayout.ObjectField("Avatar Prefab", AvatarPrefab, typeof(GameObject), false) as GameObject;

			if(AvatarPrefab != null)
				if(PrefabUtility.GetPrefabAssetType(AvatarPrefab) == PrefabAssetType.NotAPrefab)
				{
					AvatarPrefab = null;
					Debug.LogError($"Provided avatar prefab MUST be a prefab.");
				}

			if(GUILayout.Button("Build World AssetBundle"))
			{
				if(!TryAuthenticate())
				{
					Debug.LogError($"Failed to authenticate User: {AccountName}");
					return;
				}

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
				//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
				ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				ServicePointManager.CheckCertificateRevocationList = false;

				IContentServerServiceClient ucmService = Refit.RestService.For<IContentServerServiceClient>("http://192.168.0.12:5005/");

				//Done out here, must be called on the main thread
				string projectPath = Application.dataPath.ToLower().TrimEnd(@"assets".ToCharArray());

				Thread uploadThread = new Thread(new ThreadStart(async () =>
				{
					try
					{
						string uploadUrl = (await ucmService.GetNewWorldUploadUrl(AuthToken)).UploadUrl;
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
	}
}