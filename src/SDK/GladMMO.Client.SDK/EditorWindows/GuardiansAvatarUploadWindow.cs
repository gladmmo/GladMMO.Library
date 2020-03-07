using System; using FreecraftCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEditor;

namespace GladMMO.SDK
{
	public sealed class GuardiansAvatarUploadWindow : BasePrefabedCustomContentUploadEditorWindow<AvatarDefinitionData>
	{
		public GuardiansAvatarUploadWindow()
			: base(UserContentType.Avatar)
		{

		}

		[MenuItem("VRGuardians/Content/AvatarUpload")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(GuardiansAvatarUploadWindow));
		}
	}
}