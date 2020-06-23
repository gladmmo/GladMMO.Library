using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_MONSTER_MOVE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_MONSTER_MOVE_Payload>
	{
		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyEntityGuidMappable<AsyncLock> LockMappable { get; }

		private IReadonlyEntityGuidMappable<EntityMovementSpeed> MovementSpeedMappable { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> WorldTransformMappable { get; }

		public SMSG_MONSTER_MOVE_PayloadHandler(ILog logger, 
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<AsyncLock> lockMappable,
			[NotNull] IReadonlyEntityGuidMappable<EntityMovementSpeed> movementSpeedMappable,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> worldTransformMappable) 
			: base(logger)
		{
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			LockMappable = lockMappable ?? throw new ArgumentNullException(nameof(lockMappable));
			MovementSpeedMappable = movementSpeedMappable ?? throw new ArgumentNullException(nameof(movementSpeedMappable));
			WorldTransformMappable = worldTransformMappable ?? throw new ArgumentNullException(nameof(worldTransformMappable));
		}

		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_MONSTER_MOVE_Payload payload)
		{
			ObjectGuid creatureGuid = payload.MonsterGuid;

			//This reason we must lock here is to prevent overriding INITIAL movement data and generation
			//it's SO dumb but there is a race condition on Entity spawning at the same time we recieve a movement update.
			using (await LockMappable.RetrieveEntity(payload.MonsterGuid).LockAsync())
			{
				switch(payload.MoveInfo.MoveType)
				{
					case MonsterMoveType.MonsterMoveStop:
						//For STOP we basically just idle at this point, the initial point sent down in the move packet.
						//DO NOT USE REPLACE, WE MAY NOT HAVE MOVE GENERATOR YET!!

						//TODO: Provide corect angle, or make angle optional.
						ProjectVersionStage.AssertInternalTesting();
						MovementGeneratorMappable[creatureGuid] = new IdleMovementGenerator(payload.InitialMovePoint.ToUnityVector());
						break;

					//All these types have a spline.
					case MonsterMoveType.MonsterMoveNormal:
						//TODO: Handle different spline types
						if (payload.OptionalSplineInformation.HasLinearPath)
							MovementGeneratorMappable[creatureGuid] = BuildNormalLinearMovementGenerator(payload);
						break;
					case MonsterMoveType.MonsterMoveFacingSpot:
						break;
					case MonsterMoveType.MonsterMoveFacingTarget:
						if (payload.OptionalSplineInformation.HasLinearPath && payload.MoveInfo.HasFinalTarget)
						{
							//Possible we don't know they entity they are chasing.
							//So we check and if not build a normal path generator.
							if (WorldTransformMappable.ContainsKey(payload.MoveInfo.FinalTarget))
							{
								WorldTransform entity = WorldTransformMappable.RetrieveEntity(payload.MoveInfo.FinalTarget);
								MovementGeneratorMappable[creatureGuid] = new LookAtLinearPathMovementGenerator(payload.OptionalSplineInformation.OptionalLinearPathInformation, payload.InitialMovePoint.ToUnityVector(), payload.OptionalSplineInformation.SplineDuration, 0, MovementSpeedMappable.RetrieveEntity(payload.MonsterGuid), entity);
							}
							else
							{
								if(Logger.IsWarnEnabled)
									Logger.Warn($"Creature: {payload.MonsterGuid} tried to Face: {payload.MoveInfo.FinalTarget} but does not exist on client.");

								MovementGeneratorMappable[creatureGuid] = BuildNormalLinearMovementGenerator(payload);
							}
						}
						break;
					case MonsterMoveType.MonsterMoveFacingAngle:
						if (payload.OptionalSplineInformation.HasLinearPath && payload.MoveInfo.HasFinalOrientation)
							MovementGeneratorMappable[creatureGuid] = new FinalAngleLinearPathMovementGenerator(payload.OptionalSplineInformation.OptionalLinearPathInformation, payload.InitialMovePoint.ToUnityVector(), payload.OptionalSplineInformation.SplineDuration, 0, MovementSpeedMappable.RetrieveEntity(payload.MonsterGuid), payload.MoveInfo.FinalOrientation.ToUnity3DYAxisRotation());
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private LinearPathMovementGenerator BuildNormalLinearMovementGenerator(SMSG_MONSTER_MOVE_Payload payload)
		{
			return new LinearPathMovementGenerator(payload.OptionalSplineInformation.OptionalLinearPathInformation, payload.InitialMovePoint.ToUnityVector(), payload.OptionalSplineInformation.SplineDuration, MovementSpeedMappable.RetrieveEntity(payload.MonsterGuid));
		}
	}
}
