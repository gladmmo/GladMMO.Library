using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	//[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public class PreBurstSceneChangeChangerEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<IServerRequestedSceneChangeEventSubscribable, ServerRequestedSceneChangeEventArgs>
	{
		/// <inheritdoc />
		public PreBurstSceneChangeChangerEventListener(IServerRequestedSceneChangeEventSubscribable subscriptionService)
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected override void OnThreadUnSafeEventFired(object source, ServerRequestedSceneChangeEventArgs args)
		{
			//TODO: We should handle REAL scene changes here.
			int currentIndex = SceneManager.GetActiveScene().buildIndex;

			//TODO: Move to a solution that won't require a +1 (will be confusing to designers)
			//PreBurst scene should always be the last scene before playable scenes.
			int sceneIndexToLoad = currentIndex + (int)args.SceneRequested + 1; //otherwise 0th enum index.

			//TODO: Reimplement proper map/scene loading. This is just hardcoded lobby.
			SceneManager.LoadSceneAsync((int)PlayableGameScene.LobbyType1 + currentIndex + 1, LoadSceneMode.Single).allowSceneActivation = true;
		}
	}
}
