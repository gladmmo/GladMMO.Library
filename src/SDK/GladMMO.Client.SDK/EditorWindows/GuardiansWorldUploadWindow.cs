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
	public sealed class GuardiansWorldUploadWindow : AuthenticatableEditorWindow
	{
		[SerializeField]
		private UnityEngine.Object SceneObject;

		[SerializeField]
		private string AssetBundlePath;

		[MenuItem("VRGuardians/WorldUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansWorldUploadWindow));
		}

		protected override void OnGUI()
		{
			base.OnGUI();

			//TODO: Validate scene file
			SceneObject = EditorGUILayout.ObjectField("Scene", SceneObject, typeof(SceneAsset), false);

			if(GUILayout.Button("Build World AssetBundle"))
			{
				if(!TryAuthenticate())
				{
					Debug.LogError($"Failed to authenticate User: {AccountName}");
					return;
				}

				//Once authenticated we need to try to build the bundle.
				ProjectVindictiveAssetbundleBuilder builder = new ProjectVindictiveAssetbundleBuilder(SceneObject);

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

				IDownloadableContentServerServiceClient ucmService = Refit.RestService.For<IDownloadableContentServerServiceClient>("http://72.190.177.214:5005/");

				//Done out here, must be called on the main thread
				string projectPath = Application.dataPath.ToLower().TrimEnd(@"assets".ToCharArray());

				Thread uploadThread = new Thread(new ThreadStart(async () =>
				{
					string uploadUrl = (await ucmService.GetNewWorldUploadUrl(AuthToken)).UploadUrl;
					Debug.Log($"Uploading to: {uploadUrl}.");
					var cloudBlockBlob = new CloudBlockBlob(new Uri(uploadUrl));
					await cloudBlockBlob.UploadFromFileAsync(Path.Combine(projectPath, "AssetBundles", "temp", AssetBundlePath));
				}));

				uploadThread.Start();
			}
		}
	}
}