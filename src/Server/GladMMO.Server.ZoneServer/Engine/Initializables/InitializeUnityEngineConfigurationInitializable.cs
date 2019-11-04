using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeUnityEngineConfigurationInitializable : IGameInitializable
	{
		public async Task OnGameInitialized()
		{
			//Get onto the main thread.
			await new UnityYieldAwaitable();
			Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
		}
	}
}
