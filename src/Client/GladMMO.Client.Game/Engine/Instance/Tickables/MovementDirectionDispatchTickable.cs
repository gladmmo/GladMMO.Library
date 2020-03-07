using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementDirectionDispatchTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IMovementDirectionChangedListener> MovementDirectionChangeListenerMappable { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		public MovementDirectionDispatchTickable([NotNull] IReadonlyEntityGuidMappable<IMovementDirectionChangedListener> movementDirectionChangeListenerMappable,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			MovementDirectionChangeListenerMappable = movementDirectionChangeListenerMappable ?? throw new ArgumentNullException(nameof(movementDirectionChangeListenerMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		public void Tick()
		{
			//TODO: Renable movement change listener handling.
			//foreach(var listener in MovementDirectionChangeListenerMappable.EnumerateWithGuid(KnownEntities, EntityTypeId.TYPEID_PLAYER))
			//	if(MovementDataMappable[listener.EntityGuid] is MovementBlockData posChangeData)
			//		listener.ComponentValue.SetMovementDirection(posChangeData.Direction);
		}
	}
}
