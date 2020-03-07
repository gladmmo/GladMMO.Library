using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Contract for event that is published BEFORE the world representation of an Entity
	/// is deconstructed. Meaning, it still exists at the time of the calling the event.
	/// </summary>
	public interface IEntityWorldRepresentationDeconstructionStartingEventSubscribable
	{
		event EventHandler<EntityWorldRepresentationDeconstructionStartingEventArgs> OnEntityWorldRepresentationDeconstructionStarting;
	}

	public sealed class EntityWorldRepresentationDeconstructionStartingEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The entity guid of the deconstructing entity.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public EntityWorldRepresentationDeconstructionStartingEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
