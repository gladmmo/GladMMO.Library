using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityDeconstructionRequestedEventSubscribable
	{
		event EventHandler<EntityDeconstructionRequestedEventArgs> OnEntityDeconstructionRequested;
	}

	public sealed class EntityDeconstructionRequestedEventArgs : EventArgs, IEntityGuidContainer
	{
		public ObjectGuid EntityGuid { get; }

		public EntityDeconstructionRequestedEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
