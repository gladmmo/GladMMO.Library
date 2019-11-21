using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementDirectionDispatchTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IMovementDirectionChangedListener> MovementDirectionChangeListenerMappable { get; }

		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		public MovementDirectionDispatchTickable([NotNull] IReadonlyEntityGuidMappable<IMovementDirectionChangedListener> movementDirectionChangeListenerMappable,
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementDataMappable,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			MovementDirectionChangeListenerMappable = movementDirectionChangeListenerMappable ?? throw new ArgumentNullException(nameof(movementDirectionChangeListenerMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		public void Tick()
		{
			foreach(var listener in MovementDirectionChangeListenerMappable.EnumerateWithGuid(KnownEntities, EntityType.Player))
				if(MovementDataMappable[listener.EntityGuid] is PositionChangeMovementData posChangeData)
					listener.ComponentValue.SetMovementDirection(posChangeData.Direction);
		}
	}
}
