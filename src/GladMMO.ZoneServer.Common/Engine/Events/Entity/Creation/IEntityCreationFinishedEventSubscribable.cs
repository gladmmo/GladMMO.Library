using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityCreationFinishedEventSubscribable
	{
		event EventHandler<EntityCreationFinishedEventArgs> OnEntityCreationFinished;
	}

	public sealed class EntityCreationFinishedEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the creating entity.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityCreationFinishedEventArgs([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
