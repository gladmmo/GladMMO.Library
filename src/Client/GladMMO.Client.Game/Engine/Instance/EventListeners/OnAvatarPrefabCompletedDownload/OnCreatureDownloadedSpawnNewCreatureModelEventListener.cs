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
	public sealed class OnCreatureDownloadedSpawnNewCreatureModelEventListener : BaseSingleEventListenerInitializable<IContentPrefabCompletedDownloadEventSubscribable, ContentPrefabCompletedDownloadEventArgs>
	{
		private IReadonlyEntityGuidMappable<GameObject> GameObjectMappable { get; }

		private ILog Logger { get; }

		private IModelScaleStrategy ModelScaler { get; }

		public OnCreatureDownloadedSpawnNewCreatureModelEventListener(IContentPrefabCompletedDownloadEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> gameObjectMappable, [NotNull] IModelScaleStrategy modelScaler)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GameObjectMappable = gameObjectMappable ?? throw new ArgumentNullException(nameof(gameObjectMappable));
			ModelScaler = modelScaler ?? throw new ArgumentNullException(nameof(modelScaler));
		}

		protected override void OnEventFired(object source, ContentPrefabCompletedDownloadEventArgs args)
		{
			//Only interested creatures
			if (args.EntityGuid.TypeId != EntityTypeId.TYPEID_UNIT)
				return;

			if(Logger.IsInfoEnabled)
				Logger.Info($"About to create new Creature model for Entity: {args.EntityGuid}");

			try
			{
				//TODO: Handle the case of a creature CHANGING it's model ID.

				//Now we've assigned the handle, we need to actually handle the spawning/loading of the creature
				//model as a child of the parent.
				GameObject root = GameObjectMappable.RetrieveEntity(args.EntityGuid);
				GameObject newlySpawnedAvatar = InstantiateNewFromPrefab(args, root);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create Creature model for Entity: {args.EntityGuid}. Error: {e.Message}\n\nStack: {e.StackTrace}");

				throw;
			}
		}

		private GameObject InstantiateNewFromPrefab([NotNull] ContentPrefabCompletedDownloadEventArgs contentEvent, [NotNull] GameObject root)
		{
			if (contentEvent == null) throw new ArgumentNullException(nameof(contentEvent));
			if (root == null) throw new ArgumentNullException(nameof(root));

			//This should be the avatar prefab
			//and we should be on the main thread here so we can now do the spawning and such
			//Replace the current avatar root gameobject with the new one.
			GameObject newAvatarRoot = GameObject.Instantiate(contentEvent.DownloadedPrefabObject, root.transform);
			newAvatarRoot.transform.localScale = root.transform.localScale;

			//Apply creature scale, loading from DBC and dynamic unit field stuff.
			newAvatarRoot.transform.localScale *= ModelScaler.ComputeScale(contentEvent.EntityGuid);

			newAvatarRoot.transform.localPosition = Vector3.zero;
			newAvatarRoot.transform.localRotation = Quaternion.identity;
			return newAvatarRoot;
		}
	}
}
