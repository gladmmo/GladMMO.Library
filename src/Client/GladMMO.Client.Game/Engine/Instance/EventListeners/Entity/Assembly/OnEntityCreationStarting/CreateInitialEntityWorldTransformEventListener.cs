using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CreateInitialEntityWorldTransformEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityGuidMappable<WorldTransform> TransformMap { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private ILog Logger { get; }

		public CreateInitialEntityWorldTransformEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] ILog logger) : base(subscriptionService)
		{
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//Only world objects have WorldTransforms.
			if (!args.EntityGuid.IsWorldObject())
				return;

			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			//Some objects aren't living, and so MOVEINFO is unavailable
			if(movementData.IsLiving)
				TransformMap.AddObject(args.EntityGuid, new WorldTransform(movementData.MoveInfo.Position.X, movementData.MoveInfo.Position.Y, movementData.MoveInfo.Position.Z, movementData.MoveInfo.Orientation));
			else if(movementData.IsStationaryObject)
			{
				//GameObjects can have this type of movement flags set
				//if they don't move.
				StationaryMovementInfo stationaryMovementInfo = movementData.StationaryObjectMovementInformation;
				Vector3 position = stationaryMovementInfo.Position.ToUnityVector();
				float orientation = stationaryMovementInfo.Orientation.ToUnity3DYAxisRotation();

				TransformMap.AddObject(args.EntityGuid, new WorldTransform(position.x, position.y, position.z, orientation));
			}
			else if (movementData.HasUpdatePosition && movementData.IsDead) //HasUpdatePosition and IsStationaryObject is mutually exclusive
			{
				//TODO: Check if actually a corpse, GameObjects and Corpses share this. But this is actually fine to be honest, due to weird format of CorpseInfo
				CorpseInfo corpseMoveInfo = movementData.DeadMovementInformation;
				Vector3 position = corpseMoveInfo.GoLocation.ToUnityVector();
				float orientation = corpseMoveInfo.Orientation.ToUnity3DYAxisRotation();

				TransformMap.AddObject(args.EntityGuid, new WorldTransform(position.x, position.y, position.z, orientation));
			}
			else
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Unhandled WorldTransform Creation: {args.EntityGuid} Flags: {movementData.UpdateFlags}");
			}
		}
	}
}
