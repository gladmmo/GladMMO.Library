using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreatePlayerEntityActorMessage : EntityActorMessage
	{
		public NetworkEntityGuid EntityGuid { get; }

		public CreatePlayerEntityActorMessage([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
