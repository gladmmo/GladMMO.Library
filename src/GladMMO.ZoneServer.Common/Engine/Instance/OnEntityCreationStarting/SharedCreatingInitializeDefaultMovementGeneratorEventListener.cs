using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
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
		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IMovementDataUpdater<MovementBlockData> MovementBlockUpdater { get; }

		public SharedCreatingInitializeDefaultMovementGeneratorEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IMovementDataUpdater<MovementBlockData> movementBlockUpdater) 
			: base(subscriptionService)
		{
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			MovementBlockUpdater = movementBlockUpdater ?? throw new ArgumentNullException(nameof(movementBlockUpdater));
		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			//Non-world objects don't have move generators.
			if (!args.EntityGuid.IsWorldObject())
				return;

			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			MovementBlockUpdater.Update(args.EntityGuid, movementData, false);
		}
	}
}
