using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MSG_MOVE_FORCE_CHANGE_SPEED_PayloadHandler : IPeerMessageHandler<GamePacketPayload, GamePacketPayload>
	{
		private IReadonlyEntityGuidMappable<EntityMovementSpeed> MovementSpeedMappable { get; }

		public MSG_MOVE_FORCE_CHANGE_SPEED_PayloadHandler([NotNull] IReadonlyEntityGuidMappable<EntityMovementSpeed> movementSpeedMappable)
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
				case NetworkOperationCode.SMSG_FORCE_WALK_SPEED_CHANGE:      
				case NetworkOperationCode.SMSG_FORCE_RUN_SPEED_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_RUN_BACK_SPEED_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_SWIM_SPEED_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_SWIM_BACK_SPEED_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_TURN_RATE_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_FLIGHT_SPEED_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE:
				case NetworkOperationCode.SMSG_FORCE_PITCH_RATE_CHANGE:
					return true;
				default:
					return false;
			}
		}
	}
}
