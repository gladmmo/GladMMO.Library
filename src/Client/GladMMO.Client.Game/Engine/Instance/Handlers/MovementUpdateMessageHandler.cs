using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(MovementUpdateMessageHandler))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementUpdateMessageHandler : BaseGameClientGameMessageHandler<MovementDataUpdateEventPayload>
	{
		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		/// <inheritdoc />
		public MovementUpdateMessageHandler(
			ILog logger,
			[NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> movementGeneratorFactory,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable,
			[NotNull] IKnownEntitySet knownEntities,
			[NotNull] ILocalPlayerDetails playerDetails)
			: base(logger)
		{
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, MovementDataUpdateEventPayload payload)
		{
			if(!payload.HasMovementData)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Empty movement update packet received.");

				return Task.CompletedTask;
			}

			foreach(var movementUpdate in payload.MovementDatas)
			{
				HandleMovementUpdate(movementUpdate);
			}

			return Task.CompletedTask;
		}

		public void HandleMovementUpdate(EntityAssociatedData<IMovementData> movementUpdate, bool forceHandleLocal = false)
		{
			if (!KnownEntities.isEntityKnown(movementUpdate.EntityGuid))
			{
				if (Logger.IsInfoEnabled)
					Logger.Info($"TODO: Received movement update too soon. Must enable deferred movement update queueing for entities that are about to spawn.");

				return;
			}

			try
			{
				if (!forceHandleLocal)
				{
					//Cheap check, and we're on another thread so performance doesn't really matter
					if(movementUpdate.EntityGuid == PlayerDetails.LocalPlayerGuid)
						if(movementUpdate.Data.isUserCreated)
							return; //don't handle user created movement data about ourselves. It'll just make movement abit janky locally.
				}

				IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(movementUpdate);

				//We just initialize this casually, the next update tick in Unity3D will start the movement generator, the old generator actually might be running right now
				//at the time this is set.
				MovementGeneratorMappable.ReplaceObject(movementUpdate.EntityGuid, generator);
				MovementDataMappable.ReplaceObject(movementUpdate.EntityGuid, movementUpdate.Data);
			}
			catch (Exception e)
			{
				string error = $"Failed to handle Movement Data for Entity: {movementUpdate.EntityGuid} Type: {movementUpdate.Data.GetType().Name} Error: {e.Message}";

				if (Logger.IsErrorEnabled)
					Logger.Error(error);

				throw new InvalidOperationException(error, e);
			}
		}
	}
}