using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityDeconstructionFinishedEventSubscribable
	{
		event EventHandler<EntityCreationFinishedEventArgs> OnEntityDeconstructionFinished;
	}

	public sealed class EntityDeconstructionFinishedEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the deconstructing entity.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityDeconstructionFinishedEventArgs([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
