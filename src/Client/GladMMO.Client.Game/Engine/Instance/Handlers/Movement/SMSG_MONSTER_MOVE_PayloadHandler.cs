using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_MONSTER_MOVE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_MONSTER_MOVE_Payload>
	{
		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		public SMSG_MONSTER_MOVE_PayloadHandler(ILog logger, 
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IReadonlyNetworkTimeService timeService) 
			: base(logger)
		{
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_MONSTER_MOVE_Payload payload)
		{
			ObjectGuid creatureGuid = payload.MonsterGuid;

			switch (payload.MoveInfo.MoveType)
			{
				case MonsterMoveType.MonsterMoveStop:
					//For STOP we basically just idle at this point, the initial point sent down in the move packet.
					MovementGeneratorMappable.ReplaceObject(creatureGuid, new IdleMovementGenerator(payload.InitialMovePoint.ToUnityVector()));
					break;

				//All these types have a spline.
				case MonsterMoveType.MonsterMoveNormal:
					break;
				case MonsterMoveType.MonsterMoveFacingSpot:
				case MonsterMoveType.MonsterMoveFacingTarget:
				case MonsterMoveType.MonsterMoveFacingAngle:
					//TODO: Handle different spline types
					if(payload.OptionalSplineInformation.HasLinearPath)
						MovementGeneratorMappable.ReplaceObject(creatureGuid, new LinearPathMovementGenerator(payload.OptionalSplineInformation.OptionalLinearPathInformation, payload.InitialMovePoint.ToUnityVector(), payload.OptionalSplineInformation.SplineDuration));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return Task.CompletedTask;
		}
	}
}
