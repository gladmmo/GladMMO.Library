using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Event that should be subscribed to BEFORE the "destruction"
	/// of the entity. Subscribers can deal with disassembly
	/// logic and such.
	/// </summary>
	public interface IEntityDeconstructionStartingEventSubscribable
	{
		event EventHandler<EntityDeconstructionStartingEventArgs> OnEntityDeconstructionStarting;
	}

	public sealed class EntityDeconstructionStartingEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the deconstructing entity.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityDeconstructionStartingEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
