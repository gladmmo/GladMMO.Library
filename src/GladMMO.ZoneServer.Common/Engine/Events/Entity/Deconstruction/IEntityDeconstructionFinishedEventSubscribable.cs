using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityDeconstructionFinishedEventSubscribable
	{
		event EventHandler<EntityDeconstructionFinishedEventArgs> OnEntityDeconstructionFinished;
	}

	public sealed class EntityDeconstructionFinishedEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the deconstructing entity.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityDeconstructionFinishedEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
