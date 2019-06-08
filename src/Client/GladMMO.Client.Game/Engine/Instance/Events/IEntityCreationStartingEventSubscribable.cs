using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Event that should be subscribed to BEFORE the "spawning"
	/// of the entity. Subscribers can deal with initialization
	/// logic and such.
	/// </summary>
	public interface IEntityCreationStartingEventSubscribable
	{
		event EventHandler<EntityCreationEventArgs> OnEntityCreationStarting;
	}

	public sealed class EntityCreationEventArgs : EventArgs
	{
		/// <summary>
		/// The entity guid of the creating entity.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityCreationEventArgs([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
