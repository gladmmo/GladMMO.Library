using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message for actors to indicate interest is lost in an entity.
	/// </summary>
	public sealed class EntityInterestRemoveMessage : EntityActorMessage
	{
		/// <summary>
		/// The entity to remove interest for.
		/// </summary>
		public ObjectGuid Entity { get; }

		public EntityInterestRemoveMessage([NotNull] ObjectGuid entity)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
		}
	}
}
