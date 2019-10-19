using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class CreateSelectedCharacterAvatarDisplayEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<IContentPrefabCompletedDownloadEventSubscribable, ContentPrefabCompletedDownloadEventArgs>
	{
		private Action OnContentPrefabRecieved;

		public CreateSelectedCharacterAvatarDisplayEventListener(IContentPrefabCompletedDownloadEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected override void OnThreadUnSafeEventFired(object source, ContentPrefabCompletedDownloadEventArgs args)
		{
			//We really should only get this for players, so we should just assume this will work.
			OnContentPrefabRecieved?.Invoke();
			OnContentPrefabRecieved = null;

			GameObject avatar = GameObject.Instantiate(args.DownloadedPrefabObject, new Vector3(1, 0, -2.4f), Quaternion.Euler(0, 90, 0));
			avatar.transform.localScale = Vector3.one;
			avatar.layer = 5;

			OnContentPrefabRecieved += () =>
			{
				GameObject.Destroy(avatar);
				args.PrefabHandle.Release();
			};
		}
	}
}
