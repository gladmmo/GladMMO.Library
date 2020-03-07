using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityCreationRequestedEventSubscribable
	{
		event EventHandler<EntityCreationRequestedEventArgs> OnEntityCreationRequested;
	}

	public sealed class EntityCreationRequestedEventArgs : EventArgs, IEntityGuidContainer
	{
		public ObjectGuid EntityGuid { get; }

		public EntityCreationRequestedEventArgs([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
