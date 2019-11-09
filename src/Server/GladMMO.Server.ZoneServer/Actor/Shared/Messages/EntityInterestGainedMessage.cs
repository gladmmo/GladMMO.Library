using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message for actors to indicate interest is gained in an entity.
	/// </summary>
	public sealed class EntityInterestGainedMessage : EntityActorMessage
	{
		/// <summary>
		/// The entity to gain interest for.
		/// </summary>
		public NetworkEntityGuid Entity { get; }

		public EntityInterestGainedMessage([NotNull] NetworkEntityGuid entity)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
		}
	}
}
