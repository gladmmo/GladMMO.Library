using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementUpdateMessageHandler : BaseGameClientGameMessageHandler<MovementDataUpdateEventPayload>
	{
		private IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		/// <inheritdoc />
		public MovementUpdateMessageHandler(
			ILog logger,
			[NotNull] IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>> movementGeneratorFactory,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable)
			: base(logger)
		{
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
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
				try
				{
					IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(movementUpdate);

					//We just initialize this casually, the next update tick in Unity3D will start the movement generator, the old generator actually might be running right now
					//at the time this is set.
					MovementGeneratorMappable.ReplaceObject(movementUpdate.EntityGuid, generator);
					MovementDataMappable.ReplaceObject(movementUpdate.EntityGuid, movementUpdate.Data);

				}
				catch (Exception e)
				{
					string error = $"Failed to handle Movement Data for Entity: {movementUpdate.EntityGuid} Type: {movementUpdate.Data.GetType().Name}";

					if(Logger.IsErrorEnabled)
						Logger.Error(error);

					throw new InvalidOperationException(error, e);
				}
			}

			return Task.CompletedTask;
		}
	}
}