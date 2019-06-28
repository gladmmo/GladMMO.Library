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
		public NetworkEntityGuid EntityGuid { get; }

		public EntityDeconstructionRequestedEventArgs([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
