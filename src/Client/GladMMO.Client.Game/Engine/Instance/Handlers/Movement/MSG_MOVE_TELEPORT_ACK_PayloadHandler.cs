using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	//This packet is sent when the server wants to force teleport a player
	//to a specified position. The client must handle the teleport process and then send an MSG_MOVE_TELEPORT_ACK back
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MSG_MOVE_TELEPORT_ACK_PayloadHandler : BaseGameClientGameMessageHandler<MSG_MOVE_TELEPORT_ACK_Payload>
	{
		private IEntityGuidMappable<MovementInfo> MovementInfoMappable { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		/// <inheritdoc />
		public MSG_MOVE_TELEPORT_ACK_PayloadHandler([NotNull] ILog logger,
			[NotNull] IEntityGuidMappable<MovementInfo> movementInfoMappable,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable)
			: base(logger)
		{
			MovementInfoMappable = movementInfoMappable ?? throw new ArgumentNullException(nameof(movementInfoMappable));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, MSG_MOVE_TELEPORT_ACK_Payload payload)
		{
			//To ACK to the server we got this message, we just send this right back
			//Seems wacky, but next update frame we will handle this force movement.
			context.PayloadSendService.SendMessage(payload);

			//TODO: This is technically not threadsafe.
			//We 100% know that we exist, we're the local player. ACKs only sent to local player about local player.
			MovementGeneratorMappable.SyncObj.EnterWriteLock();
			try
			{
				MovementGeneratorMappable.ReplaceObject(payload.MovementGuid, new ForceTeleportMovementGenerator(payload.MoveInfo));
				MovementInfoMappable.ReplaceObject(payload.MovementGuid, payload.MoveInfo);
			}
			finally
			{
				MovementGeneratorMappable.SyncObj.ExitWriteLock();
			}

			return Task.CompletedTask;
		}
	}
}