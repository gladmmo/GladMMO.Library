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
	public sealed class GuardiansCreatureModelUploadWindow : BasePrefabedCustomContentUploadEditorWindow<CreatureDefinitionData>
	{
		public GuardiansCreatureModelUploadWindow()
			: base(UserContentType.Creature)
		{

		}

		[MenuItem("VRGuardians/Content/CreatureModelUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansCreatureModelUploadWindow));
		}
	}
}