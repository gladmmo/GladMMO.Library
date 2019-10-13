using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class InteractWithEntityActorMessage : EntityActorMessage
	{
		public NetworkEntityGuid EntityInteracting { get; }

		public InteractWithEntityActorMessage([NotNull] NetworkEntityGuid entityInteracting)
		{
			EntityInteracting = entityInteracting ?? throw new ArgumentNullException(nameof(entityInteracting));
		}
	}
}
