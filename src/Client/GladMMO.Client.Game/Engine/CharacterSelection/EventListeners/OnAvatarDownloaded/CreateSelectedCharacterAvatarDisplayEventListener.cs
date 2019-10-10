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

			GameObject avatar = GameObject.Instantiate(args.DownloadedPrefabObject, new Vector3(0, 0, -7), Quaternion.Euler(0, 180, 0));
			avatar.transform.localScale = Vector3.one;

			OnContentPrefabRecieved += () =>
			{
				GameObject.Destroy(avatar);
				args.PrefabHandle.Release();
			};
		}
	}
}
