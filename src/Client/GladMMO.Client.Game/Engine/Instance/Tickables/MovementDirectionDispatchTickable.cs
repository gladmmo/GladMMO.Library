using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementDirectionDispatchTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IMovementDirectionChangedListener> MovementDirectionChangeListenerMappable { get; }

		private IReadonlyEntityGuidMappable<MovementInfo> MovementDataMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		public MovementDirectionDispatchTickable([NotNull] IReadonlyEntityGuidMappable<IMovementDirectionChangedListener> movementDirectionChangeListenerMappable,
			[NotNull] IReadonlyEntityGuidMappable<MovementInfo> movementDataMappable,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			MovementDirectionChangeListenerMappable = movementDirectionChangeListenerMappable ?? throw new ArgumentNullException(nameof(movementDirectionChangeListenerMappable));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		public void Tick()
		{
			//This is only for Gaia Online avatars.
			//TODO: Renable movement change listener handling.
			foreach (var listener in MovementDirectionChangeListenerMappable.EnumerateWithGuid(KnownEntities, EntityTypeId.TYPEID_PLAYER))
			{
				//TODO: This is hacky, but Gaia only need to know if there is ANY movement.
				MovementInfo info = MovementDataMappable[listener.EntityGuid];
				listener.ComponentValue.SetMovementDirection(info.MoveFlags.HasAnyFlags(MovementFlag.MOVEMENTFLAG_MASK_MOVING) ? Vector2.one : Vector2.zero);
			}
		}
	}
}
