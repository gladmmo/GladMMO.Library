using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using GladMMO.FinalIK;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnGameObjectDownloadedSpawnNewCreatureModelEventListener : BaseSingleEventListenerInitializable<IContentPrefabCompletedDownloadEventSubscribable, ContentPrefabCompletedDownloadEventArgs>
	{
		private IReadonlyEntityGuidMappable<GameObject> GameObjectMappable { get; }

		private ILog Logger { get; }

		public OnGameObjectDownloadedSpawnNewCreatureModelEventListener(IContentPrefabCompletedDownloadEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> gameObjectMappable)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GameObjectMappable = gameObjectMappable ?? throw new ArgumentNullException(nameof(gameObjectMappable));
		}

		protected override void OnEventFired(object source, ContentPrefabCompletedDownloadEventArgs args)
		{
			//Only interested GameObjects
			if (args.EntityGuid.TypeId != EntityTypeId.TYPEID_GAMEOBJECT)
				return;

			if(Logger.IsInfoEnabled)
				Logger.Info($"About to create new GameObject model for Entity: {args.EntityGuid}");

			try
			{
				//TODO: Handle the case of a GameObject CHANGING it's model ID.

				//Now we've assigned the handle, we need to actually handle the spawning/loading of the GameObject
				//model as a child of the parent.
				GameObject root = GameObjectMappable.RetrieveEntity(args.EntityGuid);
				GameObject newlySpawnedAvatar = InstantiateNewFromPrefab(args.DownloadedPrefabObject, root);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create GameObject model for Entity: {args.EntityGuid}. Error: {e.Message}\n\nStack: {e.StackTrace}");

				throw;
			}
		}

		private GameObject InstantiateNewFromPrefab([NotNull] GameObject prefab, [NotNull] GameObject root)
		{
			if (prefab == null) throw new ArgumentNullException(nameof(prefab));
			if (root == null) throw new ArgumentNullException(nameof(root));

			//This should be the avatar prefab
			//and we should be on the main thread here so we can now do the spawning and such
			//Replace the current avatar root gameobject with the new one.
			GameObject newAvatarRoot = GameObject.Instantiate(prefab, root.transform);
			newAvatarRoot.transform.localScale = root.transform.localScale;
			newAvatarRoot.transform.localPosition = Vector3.zero;
			newAvatarRoot.transform.localRotation = Quaternion.identity;
			return newAvatarRoot;
		}
	}
}
