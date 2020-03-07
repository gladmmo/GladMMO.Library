using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.Azure.Storage.Blob;
using UnityEditor;
using UnityEngine;

namespace GladMMO.SDK
{
	public abstract class BaseCustomContentUploadEditorWindow : AuthenticatableEditorWindow
	{
		private UserContentType ContentType { get; }

		protected BaseCustomContentUploadEditorWindow(UserContentType contentType)
		{
			if(!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int)contentType, typeof(UserContentType));

			ContentType = contentType;
		}

		//TODO:
		protected static async Task UploadWorldContent(string assetBundlePath, string uploadUrl, string projectPath)
		{
			Debug.Log($"Uploading to: {uploadUrl}.");

			var cloudBlockBlob = new CloudBlockBlob(new Uri(uploadUrl));
			await cloudBlockBlob.UploadFromFileAsync(Path.Combine(projectPath, "AssetBundles", "temp", assetBundlePath))
				.ConfigureAwait(true);

			Debug.Log("Successfully uploaded.");
		}

		//TODO: Extract this to somewhere else
		protected static string GenerateContentBundle([NotNull] UnityEngine.Object bundleableObject)
		{
			if (bundleableObject == null) throw new ArgumentNullException(nameof(bundleableObject));

			//Once authenticated we need to try to build the bundle.
			ProjectVindictiveAssetbundleBuilder builder = new ProjectVindictiveAssetbundleBuilder(bundleableObject);

			//TODO: Handle uploading build
			AssetBundleManifest manifest = builder.BuildBundle();
			string bundlePath = manifest.GetAllAssetBundles().First();

			Debug.Log($"Generated AssetBundle with Path: {bundlePath}");

			//TODO: Refactor all this crap
			return bundlePath;
		}

		protected void OnRenderUploadGUI([NotNull] IUploadedContentDataDefinition uploadDefinition, [NotNull] UnityEngine.Object contentObject, Action<ContentUploadToken> onContentTokenCallback = null)
		{
			if (uploadDefinition == null) throw new ArgumentNullException(nameof(uploadDefinition));

			//If the content is null, we should render DISABLED buttons here
			if (contentObject == null)
			{
				EditorGUI.BeginDisabledGroup(true);
			}

			if(uploadDefinition.ContentId > 0)
			{
				EditorGUILayout.LabelField($"{ContentType} Id: {uploadDefinition.ContentId}");
				EditorGUILayout.LabelField($"{ContentType} GUID: {uploadDefinition.ContentGuid.ToString()}");

				//TODO: Conssolidate
				if(GUILayout.Button($"Update {ContentType}"))
				{
					string assetBundlePath = GenerateContentBundle(contentObject);

					var contentServerServiceClient = new DownloadableContentServiceClientFactory().Create(EmptyFactoryContext.Instance);
					UploadContentAssetBundle(assetBundlePath, new UpdatedContentUploadTokenFactory(contentServerServiceClient, uploadDefinition), onContentTokenCallback);
				}
			}
			else
			{
				//TODO: Consolidate
				//If there is no world data definition then we should allow users
				//to upload a new world.
				if(GUILayout.Button($"Upload {ContentType}"))
				{
					string assetBundlePath = GenerateContentBundle(contentObject);

					var contentServerServiceClient = new DownloadableContentServiceClientFactory().Create(EmptyFactoryContext.Instance);
					UploadContentAssetBundle(assetBundlePath, new NewContentUploadTokenFactory(contentServerServiceClient), onContentTokenCallback);
				}
			}

			//If the content is null, we should render DISABLED buttons here
			if(contentObject == null)
			{
				EditorGUI.EndDisabledGroup();
			}
		}

		private void UploadContentAssetBundle(string assetBundlePath, IContentUploadTokenFactory tokenFactory, Action<ContentUploadToken> onContentTokenCallback = null)
		{
			//Done out here, must be called on the main thread
			string projectPath = Application.dataPath.ToLower().TrimEnd(@"assets".ToCharArray());

			UnityAsyncHelper.InitializeSyncContext();

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				EditorUtility.DisplayProgressBar("Uploading Content", "Please wait until complete.", 0.5f);
				try
				{
					ResponseModel<ContentUploadToken, ContentUploadResponseCode> contentUploadToken = await tokenFactory.Create(new ContentUploadTokenFactoryContext(this.ContentType))
						.ConfigureAwait(true);

					//TODO: Handle failure.
					await UploadWorldContent(assetBundlePath, contentUploadToken.Result.UploadUrl, projectPath);

					//Call this callback, so the caller can get decide what to do with the result.
					onContentTokenCallback?.Invoke(contentUploadToken.Result);
				}
				catch(Exception e)
				{
					Debug.LogError($"Failed to upload. Reason: {e.Message}");
					throw;
				}
				finally
				{
					//Important to ALWAYS end the progress bar, even if failed.
					EditorUtility.ClearProgressBar();
				}
			}, null);
		}
	}
}
