using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MSG_MOVE_SPLINE_CHANGE_SPEED_PayloadHandler : IPeerMessageHandler<GamePacketPayload, GamePacketPayload>
	{
		private IReadonlyEntityGuidMappable<EntityMovementSpeed> MovementSpeedMappable { get; }

		public MSG_MOVE_SPLINE_CHANGE_SPEED_PayloadHandler([NotNull] IReadonlyEntityGuidMappable<EntityMovementSpeed> movementSpeedMappable)
		{
			MovementSpeedMappable = movementSpeedMappable ?? throw new ArgumentNullException(nameof(movementSpeedMappable));
		}

		private void HandleMessage([NotNull] IMovementSpeedChangePayload payload)
		{
			if (payload == null) throw new ArgumentNullException(nameof(payload));

			//Just get the unit move speed and update it
			EntityMovementSpeed entity = MovementSpeedMappable.RetrieveEntity(payload.Target);
			entity.SetMovementSpeed(payload.MoveType, payload.Speed);
		}

		public Task<bool> TryHandleMessage(IPeerMessageContext<GamePacketPayload> context, NetworkIncomingMessage<GamePacketPayload> message)
		{
			IMovementSpeedChangePayload payload = (IMovementSpeedChangePayload)message.Payload;
			HandleMessage(payload);
			return Task.FromResult(true);
		}

		public bool CanHandle(NetworkIncomingMessage<GamePacketPayload> message)
		{
			switch(message.Payload.GetOperationCode())
			{
				case NetworkOperationCode.SMSG_SPLINE_SET_WALK_SPEED:     
				case NetworkOperationCode.SMSG_SPLINE_SET_RUN_SPEED:
				case NetworkOperationCode.SMSG_SPLINE_SET_RUN_BACK_SPEED:
				case NetworkOperationCode.SMSG_SPLINE_SET_SWIM_SPEED:
				case NetworkOperationCode.SMSG_SPLINE_SET_SWIM_BACK_SPEED:
				case NetworkOperationCode.SMSG_SPLINE_SET_TURN_RATE:
				case NetworkOperationCode.SMSG_SPLINE_SET_FLIGHT_SPEED:
				case NetworkOperationCode.SMSG_SPLINE_SET_FLIGHT_BACK_SPEED:
				case NetworkOperationCode.SMSG_SPLINE_SET_PITCH_RATE:
					return true;
				default:
					return false;
			}
		}
	}
}
