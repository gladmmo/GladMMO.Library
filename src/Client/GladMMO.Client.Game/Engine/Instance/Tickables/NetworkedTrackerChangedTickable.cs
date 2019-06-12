using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class NetworkedTrackerChangedTickable : EventQueueBasedTickable<IPlayerTrackerTransformChangedEventSubscribable, PlayerTrackerTransformChangedEventArgs>
	{
		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> GameObjectDirectoryMappable { get; }

		public NetworkedTrackerChangedTickable(IPlayerTrackerTransformChangedEventSubscribable subscriptionService, ILog logger, 
			[NotNull] IReadonlyKnownEntitySet knownEntities, 
			[NotNull] IReadonlyEntityGuidMappable<EntityGameObjectDirectory> gameObjectDirectoryMappable)
			: base(subscriptionService, true, logger)
		{
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			GameObjectDirectoryMappable = gameObjectDirectoryMappable ?? throw new ArgumentNullException(nameof(gameObjectDirectoryMappable));
		}

		protected override void HandleEvent(PlayerTrackerTransformChangedEventArgs args)
		{
			//TODO: Log this
			if (!KnownEntities.isEntityKnown(args.ChangeInformation.EntityGuid))
				return;

			EntityGameObjectDirectory directory = GameObjectDirectoryMappable.RetrieveEntity(args.ChangeInformation.EntityGuid);

			//TODO: We need to support more than just the head.
			Transform cameraTransform = directory.GetGameObject(EntityGameObjectDirectory.Type.CameraRoot).transform;
			Transform rootTransform = directory.GetGameObject(EntityGameObjectDirectory.Type.Root).transform;

			cameraTransform.position = (rootTransform.position + args.ChangeInformation.Data.TrackerPositionUpdates[0]);
			cameraTransform.rotation = (rootTransform.rotation * args.ChangeInformation.Data.TrackerRotationUpdates[0]);
		}
	}
}
