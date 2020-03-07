using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	[Injectee]
	public sealed class VivoxCleanupComponent : MonoBehaviour
	{
		[Inject]
		public VivoxUnity.Client VoiceClient;

		void OnApplicationQuit()
		{
			VoiceClient.Uninitialize();
		}
	}
}
