﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
			Transform rootTransform = directory.GetGameObject(EntityGameObjectDirectory.Type.Root).transform;

			int index = 0;
			InitializeRotationIndex(args.ChangeInformation.Data.UpdateFields, NetworkMovementTrackerTypeFlags.Head, EntityGameObjectDirectory.Type.CameraRoot, args.ChangeInformation.Data.TrackerRotationUpdates, index, directory, rootTransform);
			InitializeRotationIndex(args.ChangeInformation.Data.UpdateFields, NetworkMovementTrackerTypeFlags.RightHand, EntityGameObjectDirectory.Type.RightHand, args.ChangeInformation.Data.TrackerRotationUpdates, index, directory, rootTransform);
			InitializeRotationIndex(args.ChangeInformation.Data.UpdateFields, NetworkMovementTrackerTypeFlags.LeftHand, EntityGameObjectDirectory.Type.LeftHand, args.ChangeInformation.Data.TrackerRotationUpdates, index, directory, rootTransform);

			index = 0;
			InitializePositionIndex(args.ChangeInformation.Data.UpdateFields, NetworkMovementTrackerTypeFlags.Head, EntityGameObjectDirectory.Type.CameraRoot, args.ChangeInformation.Data.TrackerPositionUpdates, index, directory, rootTransform);
			InitializePositionIndex(args.ChangeInformation.Data.UpdateFields, NetworkMovementTrackerTypeFlags.RightHand, EntityGameObjectDirectory.Type.RightHand, args.ChangeInformation.Data.TrackerPositionUpdates, index, directory, rootTransform);
			InitializePositionIndex(args.ChangeInformation.Data.UpdateFields, NetworkMovementTrackerTypeFlags.LeftHand, EntityGameObjectDirectory.Type.LeftHand, args.ChangeInformation.Data.TrackerPositionUpdates, index, directory, rootTransform);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int InitializeRotationIndex(NetworkMovementTrackerTypeFlags context, NetworkMovementTrackerTypeFlags flagToCheck, 
			EntityGameObjectDirectory.Type gameObjectType, Quaternion[] rotations, int index, 
			EntityGameObjectDirectory directory, Transform rootGameObjectTransform)
		{
			if((context & flagToCheck) != 0)
				directory.GetGameObject(gameObjectType).transform.rotation = rootGameObjectTransform.rotation * rotations[index++];

			return index;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int InitializePositionIndex(NetworkMovementTrackerTypeFlags context, NetworkMovementTrackerTypeFlags flagToCheck, 
			EntityGameObjectDirectory.Type gameObjectType, Vector3[] positions, int index, 
			EntityGameObjectDirectory directory, Transform rootGameObjectTransform)
		{
			//cameraTransform.position = (rootTransform.position + args.ChangeInformation.Data.TrackerPositionUpdates[0]);
			if((context & flagToCheck) != 0)
				directory.GetGameObject(gameObjectType).transform.position = rootGameObjectTransform.position + positions[index];

			return index;
		}
	}
}
