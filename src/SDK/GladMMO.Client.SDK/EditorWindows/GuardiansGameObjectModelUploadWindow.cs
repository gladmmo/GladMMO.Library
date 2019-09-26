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
	public sealed class GuardiansGameObjectModelUploadWindow : BasePrefabedCustomContentUploadEditorWindow<GameObjectDefinitionData>
	{
		public GuardiansGameObjectModelUploadWindow()
			: base(UserContentType.GameObject)
		{

		}

		[MenuItem("VRGuardians/Content/GameObjectModelUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansGameObjectModelUploadWindow));
		}
	}
}