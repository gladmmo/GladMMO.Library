using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnAvatarDownloadedSpawnNewAvatarGameObjectEventListener : BaseSingleEventListenerInitializable<IAvatarPrefabCompletedDownloadEventSubscribable, AvatarPrefabCompletedDownloadEventArgs>
	{
		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> GameObjectDirectoryMappable { get; }

		private ILog Logger { get; }

		public OnAvatarDownloadedSpawnNewAvatarGameObjectEventListener(IAvatarPrefabCompletedDownloadEventSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, AvatarPrefabCompletedDownloadEventArgs args)
		{
			//Now we've assigned the handle, we need to actually handle the spawning/loading of the avatar.
			GameObject ikRootGameObject = GameObjectDirectoryMappable.RetrieveEntity(args.EntityGuid).GetGameObject(EntityGameObjectDirectory.Type.IKRoot);
			GameObject currentAvatarRootGameObject = ikRootGameObject.transform.GetChild(0).gameObject;
			GameObject newlySpawnedAvatar = InstantiateNewFromPrefab(args.DownloadedPrefabObject, currentAvatarRootGameObject);

			GameObject.DestroyImmediate(currentAvatarRootGameObject, false);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Instantiating new Avatar for Entity: {args.EntityGuid}");
		}

		private GameObject InstantiateNewFromPrefab(GameObject prefab, GameObject oldAvatar)
		{
			//This should be the avatar prefab
			//and we should be on the main thread here so we can now do the spawning and such
			//Replace the current avatar root gameobject with the new one.
			GameObject newAvatarRoot = GameObject.Instantiate(prefab, oldAvatar.transform.parent);
			newAvatarRoot.transform.localScale = oldAvatar.transform.localScale;
			newAvatarRoot.transform.localPosition = Vector3.zero;
			newAvatarRoot.transform.localRotation = Quaternion.identity;
			return newAvatarRoot;
		}
	}
}
