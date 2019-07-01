using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[GameInitializableOrdering(2)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class MovementSimulationTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> MovementGenerators { get; }

		private IReadonlyEntityGuidMappable<GameObject> WorldObjectMap { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		/// <inheritdoc />
		public MovementSimulationTickable(
			IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerators,
			IReadonlyEntityGuidMappable<GameObject> worldObjectMap,
			INetworkTimeService timeService,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			MovementGenerators = movementGenerators ?? throw new ArgumentNullException(nameof(movementGenerators));
			WorldObjectMap = worldObjectMap ?? throw new ArgumentNullException(nameof(worldObjectMap));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		/// <inheritdoc />
		public void Tick()
		{
			long currentTime = DateTime.UtcNow.Ticks;

			foreach(var entry in MovementGenerators.EnumerateWithGuid(KnownEntities))
				entry.ComponentValue.Update(WorldObjectMap.RetrieveEntity(entry.EntityGuid), currentTime);
		}
	}
}
