using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreateCreatureEntityActorMessage : EntityActorMessage
	{
		public ObjectGuid EntityGuid { get; }

		public CreateCreatureEntityActorMessage([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
