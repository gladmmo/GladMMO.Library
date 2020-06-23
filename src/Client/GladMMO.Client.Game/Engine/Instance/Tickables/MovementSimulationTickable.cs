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

		private IReadonlyEntityGuidMappable<WorldTransform> TransformMap { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		/// <inheritdoc />
		public MovementSimulationTickable(
			IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerators,
			IReadonlyEntityGuidMappable<GameObject> worldObjectMap,
			INetworkTimeService timeService,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> transformMap)
		{
			MovementGenerators = movementGenerators ?? throw new ArgumentNullException(nameof(movementGenerators));
			WorldObjectMap = worldObjectMap ?? throw new ArgumentNullException(nameof(worldObjectMap));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//Don't pass in different remote time, large amounts of objects may get a millisecond or two more time drift from the start.
			long currentRemoteTime = TimeService.CurrentRemoteTime;

			foreach (var entry in MovementGenerators.EnumerateWithGuid(KnownEntities))
			{
				entry.ComponentValue.Update(WorldObjectMap.RetrieveEntity(entry.EntityGuid), currentRemoteTime);

				//TODO: Rotation is not correct.
				Vector3 currentPosition = entry.ComponentValue.CurrentPosition;
				WorldTransform transform = TransformMap.RetrieveEntity(entry.EntityGuid);
				transform.PositionX = currentPosition.x;
				transform.PositionY = currentPosition.y;
				transform.PositionZ = currentPosition.z;
			}
		}
	}
}
