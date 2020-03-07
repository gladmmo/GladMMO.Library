using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class EntityActorSelectedMessage : EntityActorMessage
	{
		/// <summary>
		/// Guid of the entity attempting to select us.
		/// </summary>
		public ObjectGuid SelectorGuid { get; }

		public EntityActorSelectedMessage([NotNull] ObjectGuid selectorGuid)
		{
			SelectorGuid = selectorGuid ?? throw new ArgumentNullException(nameof(selectorGuid));
		}
	}
}
