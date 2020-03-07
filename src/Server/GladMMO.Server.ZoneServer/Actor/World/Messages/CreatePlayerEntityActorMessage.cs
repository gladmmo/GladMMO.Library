using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreatePlayerEntityActorMessage : EntityActorMessage
	{
		public ObjectGuid EntityGuid { get; }

		public CreatePlayerEntityActorMessage([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
