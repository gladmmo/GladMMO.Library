using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class CreateGameObjectEntityActorMessage : EntityActorMessage
	{
		public ObjectGuid EntityGuid { get; }

		public CreateGameObjectEntityActorMessage([NotNull] ObjectGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
