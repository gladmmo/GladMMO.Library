using System; using FreecraftCore;
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

		private IEntityGuidMappable<WorldTransform> TransformMap { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyKnownEntitySet KnonwnEntities { get; }

		/// <inheritdoc />
		public MovementSimulationTickable(
			IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerators,
			IReadonlyEntityGuidMappable<GameObject> worldObjectMap,
			INetworkTimeService timeService,
			[NotNull] IReadonlyKnownEntitySet knonwnEntities,
			[NotNull] IEntityGuidMappable<WorldTransform> transformMap)
		{
			MovementGenerators = movementGenerators ?? throw new ArgumentNullException(nameof(movementGenerators));
			WorldObjectMap = worldObjectMap ?? throw new ArgumentNullException(nameof(worldObjectMap));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			KnonwnEntities = knonwnEntities ?? throw new ArgumentNullException(nameof(knonwnEntities));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//Don't pass in different remote time, large amounts of objects may get a millisecond or two more time drift from the start.
			long currentRemoteTime = TimeService.CurrentRemoteTime;

			foreach (var entry in MovementGenerators.EnumerateWithGuid(KnonwnEntities))
			{
				entry.ComponentValue.Update(WorldObjectMap.RetrieveEntity(entry.EntityGuid), currentRemoteTime);

				Vector3 currentPosition = entry.ComponentValue.CurrentPosition;
				TransformMap.ReplaceObject(entry.EntityGuid, new WorldTransform(currentPosition.x, currentPosition.y, currentPosition.z, TransformMap.RetrieveEntity(entry.EntityGuid).YAxisRotation));
			}
		}
	}
}
