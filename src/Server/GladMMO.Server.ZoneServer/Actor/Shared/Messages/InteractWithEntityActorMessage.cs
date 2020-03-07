using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class InteractWithEntityActorMessage : EntityActorMessage
	{
		public ObjectGuid EntityInteracting { get; }

		public InteractWithEntityActorMessage([NotNull] ObjectGuid entityInteracting)
		{
			EntityInteracting = entityInteracting ?? throw new ArgumentNullException(nameof(entityInteracting));
		}
	}
}
