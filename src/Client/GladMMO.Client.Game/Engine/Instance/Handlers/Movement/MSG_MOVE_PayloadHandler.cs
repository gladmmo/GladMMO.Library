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
			NetworkOperationCode code = message.Payload.GetOperationCode();

			if (code >= NetworkOperationCode.MSG_MOVE_START_FORWARD && code <= NetworkOperationCode.MSG_MOVE_SET_PITCH)
				return true;
			else
				return false;
		}

		public Task<bool> TryHandleMessage(IPeerMessageContext<GamePacketPayload> context, NetworkIncomingMessage<GamePacketPayload> message)
		{
			HandleMovementPacket((IPlayerMovementPayload<MovementInfo, MovementFlag, PackedGuid>) message.Payload);
			return Task.FromResult(true);
		}

		public void HandleMovementPacket([NotNull] IPlayerMovementPayload<MovementInfo, MovementFlag, PackedGuid> movementUpdate, bool forceHandleLocal = false)
		{
			if (movementUpdate == null) throw new ArgumentNullException(nameof(movementUpdate));

			MovementInfoMappable.ReplaceObject(movementUpdate.MovementGuid, movementUpdate.MoveInfo);

			if(!KnownEntities.isEntityKnown(movementUpdate.MovementGuid))
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"TODO: Received movement update too soon. Must enable deferred movement update queueing for entities that are about to spawn.");

				return;
			}

			try
			{
				if(!forceHandleLocal)
				{
					//TODO: Handle remote serverside controlled movement.
					//Cheap check, and we're on another thread so performance doesn't really matter
					if(movementUpdate.MovementGuid == PlayerDetails.LocalPlayerGuid)
							return; //don't handle user created movement data about ourselves. It'll just make movement abit janky locally.
				}

				IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<MovementInfo>(movementUpdate.MovementGuid, movementUpdate.MoveInfo));

				//We just initialize this casually, the next update tick in Unity3D will start the movement generator, the old generator actually might be running right now
				//at the time this is set.
				MovementGeneratorMappable.ReplaceObject(movementUpdate.MovementGuid, generator);
				MovementInfoMappable.ReplaceObject(movementUpdate.MovementGuid, movementUpdate.MoveInfo);
			}
			catch(Exception e)
			{
				string error = $"Failed to handle Movement Data for Entity: {movementUpdate.MovementGuid} Type: {movementUpdate.MoveInfo.GetType().Name} Error: {e.Message}";

				if(Logger.IsErrorEnabled)
					Logger.Error(error);

				throw new InvalidOperationException(error, e);
			}
		}

		//TODO: Make interface.
		public void SpoofLocal(GamePacketPayload payloadToSend)
		{
			HandleMovementPacket((IPlayerMovementPayload<MovementInfo, MovementFlag, PackedGuid>)payloadToSend, true);
		}
	}
}
