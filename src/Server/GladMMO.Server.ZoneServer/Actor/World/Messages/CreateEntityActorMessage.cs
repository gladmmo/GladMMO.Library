using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreateEntityActorMessage : EntityActorMessage
	{
		public ObjectGuid EntityGuid { get; }

		public CreateEntityActorMessage([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
