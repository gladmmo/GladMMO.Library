﻿using System; using FreecraftCore;
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
		event EventHandler<EntityCreationStartingEventArgs> OnEntityCreationStarting;
	}

	public sealed class EntityCreationStartingEventArgs : EventArgs
	{
		/// <summary>
		/// The entity guid of the creating entity.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityCreationStartingEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
