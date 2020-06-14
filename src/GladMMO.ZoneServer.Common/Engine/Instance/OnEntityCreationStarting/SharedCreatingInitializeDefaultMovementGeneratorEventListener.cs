using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Fasterflect;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Base event listener that handles movement generator initialization
	/// for creating entities.
	/// </summary>
	public class SharedCreatingInitializeDefaultMovementGeneratorEventListener : BaseSingleEventListenerInitializable<IEntityCreationFinishedEventSubscribable, EntityCreationFinishedEventArgs>
	{
		private ILog Logger { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IMovementDataUpdater<MovementBlockData> MovementBlockUpdater { get; }

		private IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		public SharedCreatingInitializeDefaultMovementGeneratorEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IMovementDataUpdater<MovementBlockData> movementBlockUpdater,
			[NotNull] IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			MovementBlockUpdater = movementBlockUpdater ?? throw new ArgumentNullException(nameof(movementBlockUpdater));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			//Non-world objects don't have move generators.
			if (!args.EntityGuid.IsWorldObject())
				return;

			//We check here because we may actually already have a movement generator
			//and this could happen if network thread already has a move update
			//before we spawn so we just DON'T update because the move data is stall.
			if (MovementGeneratorMappable.ContainsKey(args.EntityGuid))
			{
				if (Logger.IsWarnEnabled)
					Logger.Warn($"Entity: {args.EntityGuid} already had movement generator. NOT A BUG, RARE SPAWN/MOVE packet handling at same time.");

				return;
			}
				

			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			MovementBlockUpdater.Update(args.EntityGuid, movementData, false);
		}
	}
}
