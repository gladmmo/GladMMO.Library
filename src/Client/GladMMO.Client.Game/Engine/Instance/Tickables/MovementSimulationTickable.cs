using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementSimulationTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> MovementGenerators { get; }

		private IReadonlyEntityGuidMappable<GameObject> WorldObjectMap { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyKnownEntitySet KnonwnEntities { get; }

		/// <inheritdoc />
		public MovementSimulationTickable(
			IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerators,
			IReadonlyEntityGuidMappable<GameObject> worldObjectMap,
			INetworkTimeService timeService,
			[NotNull] IReadonlyKnownEntitySet knonwnEntities)
		{
			MovementGenerators = movementGenerators ?? throw new ArgumentNullException(nameof(movementGenerators));
			WorldObjectMap = worldObjectMap ?? throw new ArgumentNullException(nameof(worldObjectMap));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			KnonwnEntities = knonwnEntities ?? throw new ArgumentNullException(nameof(knonwnEntities));
		}

		/// <inheritdoc />
		public void Tick()
		{
			foreach(var entry in MovementGenerators)
			{
				//Entity may be in the cleanup process, or something. Shouldn't happen on client though.
				if (!KnonwnEntities.isEntityKnown(entry.Key))
					continue;

				entry.Value.Update(WorldObjectMap.RetrieveEntity(entry.Key), TimeService.CurrentRemoteTime);
			}
		}
	}
}
