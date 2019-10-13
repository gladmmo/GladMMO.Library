using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreateCreatureEntityActorMessage : EntityActorMessage
	{
		public NetworkEntityGuid EntityGuid { get; }

		public CreateCreatureEntityActorMessage([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
