using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for event that is published AFTER the world representation of an Entity
	/// is deconstructed. Meaning, it no longer exists after this event is called.
	/// Ex. Unity3D GameObject has been destroyed.
	/// </summary>
	public interface IEntityWorldRepresentationDeconstructionFinishedEventSubscribable
	{
		event EventHandler<EntityWorldRepresentationDeconstructionFinishedEventArgs> OnEntityWorldRepresentationDeconstructionFinished;
	}

	public sealed class EntityWorldRepresentationDeconstructionFinishedEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the deconstructing entity.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityWorldRepresentationDeconstructionFinishedEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
