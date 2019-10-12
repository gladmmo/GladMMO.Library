using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class CreateEntityActorMessage : EntityActorMessage
	{
		public NetworkEntityGuid EntityGuid { get; }

		public CreateEntityActorMessage([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
