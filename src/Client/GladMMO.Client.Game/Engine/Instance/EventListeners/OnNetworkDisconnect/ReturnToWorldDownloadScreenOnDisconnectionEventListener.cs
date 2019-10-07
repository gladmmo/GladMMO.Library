using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ReturnToWorldDownloadScreenOnDisconnectionEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>
	{
		public ReturnToWorldDownloadScreenOnDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			//TODO: Use the scene manager service.
			//TODO: Don't hardcode scene ids. Don't load scenes directly.
			SceneManager.LoadSceneAsync(GladMMOClientConstants.WORLD_DOWNLOAD_SCENE_NAME).allowSceneActivation = true;
		}
	}
}
