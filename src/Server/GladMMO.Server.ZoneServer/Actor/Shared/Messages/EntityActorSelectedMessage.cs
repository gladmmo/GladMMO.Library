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
		public NetworkEntityGuid SelectorGuid { get; }

		public EntityActorSelectedMessage([NotNull] NetworkEntityGuid selectorGuid)
		{
			SelectorGuid = selectorGuid ?? throw new ArgumentNullException(nameof(selectorGuid));
		}
	}
}
