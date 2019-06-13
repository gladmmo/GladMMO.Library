using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for event publisher that
	/// publishes an event when an entity has started creation.
	/// </summary>
	public interface IEntityCreatingEventSubscribable
	{
		event EventHandler<EntityCreatingEventArgs> OnEntityCreating;
	}

	public sealed class EntityCreatingEventArgs : EventArgs
	{
		/// <summary>
		/// The entity guid of the entity starting creating.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		public EntityCreatingEventArgs([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
