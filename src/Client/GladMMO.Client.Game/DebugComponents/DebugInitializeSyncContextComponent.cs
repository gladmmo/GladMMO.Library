using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class DebugInitializeSyncContextComponent : MonoBehaviour
	{
		private void Awake()
		{
			UnityAsyncHelper.InitializeSyncContext();
		}
	}
}
