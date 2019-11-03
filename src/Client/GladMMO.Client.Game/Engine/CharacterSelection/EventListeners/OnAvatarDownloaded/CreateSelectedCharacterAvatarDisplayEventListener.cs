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

			Animator animator = avatar.GetComponent<Animator>();

			if (animator != null)
				animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("IdleCharacterScreenAnimationController");
			else
			{
				animator = avatar.GetComponentInChildren<Animator>();

				if(animator != null)
					animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("IdleCharacterScreenAnimationController");
			}

			//He'll drift across the screen if we don't do this.
			//Slowly but surely.
			if (animator != null)
				animator.applyRootMotion = false;

			OnContentPrefabRecieved += () =>
			{
				GameObject.Destroy(avatar);
				args.PrefabHandle.Release();
			};
		}
	}
}
