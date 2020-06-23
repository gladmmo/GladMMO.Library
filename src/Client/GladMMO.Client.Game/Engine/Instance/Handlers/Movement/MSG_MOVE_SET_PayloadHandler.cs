using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MSG_MOVE_SET_PayloadHandler : IPeerMessageHandler<GamePacketPayload, GamePacketPayload>
	{
		private IReadonlyEntityGuidMappable<EntityMovementSpeed> MovementSpeedMappable { get; }

		private MSG_MOVE_PayloadHandler MoveHandler { get; }

		public MSG_MOVE_SET_PayloadHandler([NotNull] IReadonlyEntityGuidMappable<EntityMovementSpeed> movementSpeedMappable,
			[NotNull] MSG_MOVE_PayloadHandler moveHandler)
		{
			MovementSpeedMappable = movementSpeedMappable ?? throw new ArgumentNullException(nameof(movementSpeedMappable));
			MoveHandler = moveHandler ?? throw new ArgumentNullException(nameof(moveHandler));
		}

		private void HandleMessage([NotNull] IMovementSpeedChangeOtherPayload payload)
		{
			if (payload == null) throw new ArgumentNullException(nameof(payload));

			//Just get the unit move speed and update it, then we can process the movement block too.
			EntityMovementSpeed entity = MovementSpeedMappable.RetrieveEntity(payload.Target);
			entity.SetMovementSpeed(payload.MoveType, payload.Speed);

			//We don't ever send local updates through these opcodes so we don't have to spoof local
			//or anything. We just need to handle and update the move info now that the new speed is set.
			MoveHandler.HandleMovementInfo(payload.Target, payload.MovementInformation, false);
		}

		public Task<bool> TryHandleMessage(IPeerMessageContext<GamePacketPayload> context, NetworkIncomingMessage<GamePacketPayload> message)
		{
			IMovementSpeedChangeOtherPayload payload = (IMovementSpeedChangeOtherPayload)message.Payload;
			HandleMessage(payload);
			return Task.FromResult(true);
		}

		public bool CanHandle(NetworkIncomingMessage<GamePacketPayload> message)
		{
			/*MSG_MOVE_SET_WALK_SPEED
			MSG_MOVE_SET_RUN_SPEED
			MSG_MOVE_SET_RUN_BACK_SPEED
			MSG_MOVE_SET_SWIM_SPEED
			MSG_MOVE_SET_SWIM_BACK_SPEED
			MSG_MOVE_SET_TURN_RATE
			MSG_MOVE_SET_FLIGHT_SPEED
			MSG_MOVE_SET_FLIGHT_BACK_SPEED
			MSG_MOVE_SET_PITCH_RATE*/

			switch(message.Payload.GetOperationCode())
			{
				case NetworkOperationCode.MSG_MOVE_SET_WALK_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_RUN_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_RUN_BACK_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_SWIM_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_SWIM_BACK_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_TURN_RATE:
				case NetworkOperationCode.MSG_MOVE_SET_FLIGHT_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_FLIGHT_BACK_SPEED:
				case NetworkOperationCode.MSG_MOVE_SET_PITCH_RATE:
					return true;
				default:
					return false;
			}
		}
	}
}
