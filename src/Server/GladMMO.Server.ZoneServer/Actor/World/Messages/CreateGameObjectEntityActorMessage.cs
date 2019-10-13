using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreateGameObjectEntityActorMessage : EntityActorMessage
	{
		public NetworkEntityGuid EntityGuid { get; }

		public CreateGameObjectEntityActorMessage([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
