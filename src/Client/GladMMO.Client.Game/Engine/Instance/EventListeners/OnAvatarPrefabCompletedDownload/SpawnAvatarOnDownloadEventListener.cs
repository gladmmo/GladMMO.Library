﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEntityAvatarChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SpawnAvatarOnDownloadEventListener : ContentPrefabCompletedDownloadEventListener, IEntityAvatarChangedEventSubscribable
	{
		private IReadonlyEntityGuidMappable<GameObject> GameObjectMappable { get; }

		private ILog Logger { get; }

		private IModelScaleStrategy ModelScaler { get; }

		private IReadonlyKnownEntitySet KnownEntitySet { get; }

		public event EventHandler<EntityAvatarChangedEventArgs> OnEntityAvatarChanged;

		public SpawnAvatarOnDownloadEventListener(IContentPrefabCompletedDownloadEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> gameObjectMappable,
			[NotNull] IModelScaleStrategy modelScaler,
			[NotNull] IReadonlyKnownEntitySet knownEntitySet)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GameObjectMappable = gameObjectMappable ?? throw new ArgumentNullException(nameof(gameObjectMappable));
			ModelScaler = modelScaler ?? throw new ArgumentNullException(nameof(modelScaler));
			KnownEntitySet = knownEntitySet ?? throw new ArgumentNullException(nameof(knownEntitySet));
		}

		protected override void OnEventFired(object source, ContentPrefabCompletedDownloadEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"About to create new GameObject model for Entity: {args.EntityGuid}");

			//Possible it was despawned before we finished loading it.
			if(!KnownEntitySet.isEntityKnown(args.EntityGuid))
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"No longer known Entity: {args.EntityGuid} finished loading model Id: {args.DownloadedPrefabObject.name}");

				return;
			}

			try
			{
				//Now we've assigned the handle, we need to actually handle the spawning/loading of the GameObject
				//model as a child of the parent.
				GameObject root = GameObjectMappable.RetrieveEntity(args.EntityGuid);

				//TODO: This is kinda hacky, we should maintain a management component that provides access to these things.
				//Destroy existing one
				foreach(Transform transform in root.transform)
				{
					if (transform.gameObject.name == "VISUAL_MODEL")
					{
						GameObject.Destroy(transform.gameObject);
						break;
					}
				}

				GameObject newlySpawnedAvatar = InstantiateNewFromPrefab(args.EntityGuid, args.DownloadedPrefabObject, root);
				newlySpawnedAvatar.name = "VISUAL_MODEL";

				//Broacast and Avatar/Model change to the framework.
				OnEntityAvatarChanged?.Invoke(this, new EntityAvatarChangedEventArgs(args.EntityGuid, newlySpawnedAvatar));
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create GameObject model for Entity: {args.EntityGuid}. Error: {e.Message}\n\nStack: {e.StackTrace}");

				throw;
			}
		}

		private GameObject InstantiateNewFromPrefab(ObjectGuid guid, [NotNull] GameObject prefab, [NotNull] GameObject root)
		{
			if(prefab == null) throw new ArgumentNullException(nameof(prefab));
			if(root == null) throw new ArgumentNullException(nameof(root));

			//This should be the avatar prefab
			//and we should be on the main thread here so we can now do the spawning and such
			//Replace the current avatar root gameobject with the new one.
			GameObject newAvatarRoot = GameObject.Instantiate(prefab, root.transform);
			newAvatarRoot.transform.localScale = root.transform.localScale;

			//Apply creature scale, loading from DBC and dynamic unit field stuff.
			newAvatarRoot.transform.localScale *= ModelScaler.ComputeScale(guid);

			newAvatarRoot.transform.localPosition = Vector3.zero;
			newAvatarRoot.transform.localRotation = Quaternion.identity;
			return newAvatarRoot;
		}
	}
}