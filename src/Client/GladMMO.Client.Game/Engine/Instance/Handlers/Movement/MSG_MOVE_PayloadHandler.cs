using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// General move packet handler for Move protocol.
	/// </summary>
	[AdditionalRegisterationAs(typeof(MSG_MOVE_PayloadHandler))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MSG_MOVE_PayloadHandler : IPeerMessageHandler<GamePacketPayload, GamePacketPayload>
	{
		private IEntityGuidMappable<MovementInfo> MovementInfoMappable { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> MovementGeneratorFactory { get; }

		private ILog Logger { get; }

		public MSG_MOVE_PayloadHandler([NotNull] IEntityGuidMappable<MovementInfo> movementInfoMappable, 
			IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable, 
			IReadonlyKnownEntitySet knownEntities, 
			ILocalPlayerDetails playerDetails,
			[NotNull] ILog logger,
			[NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<MovementInfo>> movementGeneratorFactory)
		{
			MovementInfoMappable = movementInfoMappable ?? throw new ArgumentNullException(nameof(movementInfoMappable));
			MovementGeneratorMappable = movementGeneratorMappable;
			KnownEntities = knownEntities;
			PlayerDetails = playerDetails;
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
		}

		public bool CanHandle(NetworkIncomingMessage<GamePacketPayload> message)
		{
			//TODO: Change to IsMovementPacket.
			NetworkOperationCode code = message.Payload.GetOperationCode();

			switch (code)
			{
				case NetworkOperationCode.CMSG_MOVE_FALL_RESET:
				case NetworkOperationCode.CMSG_MOVE_SET_FLY:
				case NetworkOperationCode.MSG_MOVE_FALL_LAND:
				case NetworkOperationCode.MSG_MOVE_HEARTBEAT:
				case NetworkOperationCode.MSG_MOVE_JUMP:
				case NetworkOperationCode.MSG_MOVE_SET_FACING:
				case NetworkOperationCode.MSG_MOVE_SET_PITCH:
				case NetworkOperationCode.MSG_MOVE_SET_RUN_MODE:
				case NetworkOperationCode.MSG_MOVE_SET_WALK_MODE:
				case NetworkOperationCode.MSG_MOVE_START_ASCEND:
				case NetworkOperationCode.MSG_MOVE_START_BACKWARD:
				case NetworkOperationCode.MSG_MOVE_START_DESCEND:
				case NetworkOperationCode.MSG_MOVE_START_FORWARD:
				case NetworkOperationCode.MSG_MOVE_START_PITCH_DOWN:
				case NetworkOperationCode.MSG_MOVE_START_PITCH_UP:
				case NetworkOperationCode.MSG_MOVE_START_STRAFE_LEFT:
				case NetworkOperationCode.MSG_MOVE_START_STRAFE_RIGHT:
				case NetworkOperationCode.MSG_MOVE_START_SWIM:
				case NetworkOperationCode.MSG_MOVE_START_TURN_LEFT:
				case NetworkOperationCode.MSG_MOVE_START_TURN_RIGHT:
				case NetworkOperationCode.MSG_MOVE_STOP_ASCEND:
				case NetworkOperationCode.MSG_MOVE_STOP:
				case NetworkOperationCode.MSG_MOVE_STOP_STRAFE:
				case NetworkOperationCode.MSG_MOVE_STOP_SWIM:
				case NetworkOperationCode.MSG_MOVE_STOP_TURN:
					return true;
				default:
					return false;
			}
		}

		public Task<bool> TryHandleMessage(IPeerMessageContext<GamePacketPayload> context, NetworkIncomingMessage<GamePacketPayload> message)
		{
			HandleMovementPacket((IPlayerMovementPayload<MovementInfo, MovementFlag, PackedGuid>) message.Payload);
			return Task.FromResult(true);
		}

		public void HandleMovementPacket([NotNull] IPlayerMovementPayload<MovementInfo, MovementFlag, PackedGuid> movementUpdate, bool forceHandleLocal = false)
		{
			if (movementUpdate == null) throw new ArgumentNullException(nameof(movementUpdate));

			HandleMovementInfo(movementUpdate.MovementGuid, movementUpdate.MoveInfo, forceHandleLocal);
		}

		public void HandleMovementInfo(ObjectGuid guid, MovementInfo moveInfo, bool forceHandleLocal)
		{
			MovementInfoMappable.ReplaceObject(guid, moveInfo);

			if (!KnownEntities.isEntityKnown(guid))
			{
				if (Logger.IsInfoEnabled)
					Logger.Info($"TODO: Received movement update too soon. Must enable deferred movement update queueing for entities that are about to spawn.");

				return;
			}

			try
			{
				if (!forceHandleLocal)
				{
					//TODO: Handle remote serverside controlled movement.
					//Cheap check, and we're on another thread so performance doesn't really matter
					if (guid == PlayerDetails.LocalPlayerGuid)
						return;
				}

				IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<MovementInfo>(guid, moveInfo));

				//We just initialize this casually, the next update tick in Unity3D will start the movement generator, the old generator actually might be running right now
				//at the time this is set.
				MovementGeneratorMappable.ReplaceObject(guid, generator);
				MovementInfoMappable.ReplaceObject(guid, moveInfo);
			}
			catch (Exception e)
			{
				string error = $"Failed to handle Movement Data for Entity: {guid} Type: {moveInfo.GetType().Name} Error: {e.Message}";

				if (Logger.IsErrorEnabled)
					Logger.Error(error);

				throw new InvalidOperationException(error, e);
			}
		}

		//TODO: Make interface.
		public void SpoofLocal(GamePacketPayload payloadToSend)
		{
			//We unfortunately most lock on local spoofs because network thread
			//may be setting an actual uninterruptable movement generator
			//Otherwise we'd have a race condition.
			MovementGeneratorMappable.SyncObj.EnterReadLock();
			try
			{
				//Cannot spoof a local movement
				//if a generator is running that disallows client movement input
				if (MovementGeneratorMappable.ContainsKey(PlayerDetails.LocalPlayerGuid))
				{
					IMovementGenerator<GameObject> currentGenerator = MovementGeneratorMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

					//REJECT client movement if we cannot interrupt the current generator
					//and it's not finished. Finished uninterruptable generators can be overriden.
					if (!currentGenerator.IsClientInterruptable && !currentGenerator.isFinished)
						return;
				}
			}
			finally
			{
				MovementGeneratorMappable.SyncObj.ExitReadLock();
			}

			HandleMovementPacket((IPlayerMovementPayload<MovementInfo, MovementFlag, PackedGuid>)payloadToSend, true);
		}
	}
}
