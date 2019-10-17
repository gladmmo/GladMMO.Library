using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultEntityActorStateContainer : IEntityActorStateContainable
	{
		public IEntityDataFieldContainer EntityData { get; }

		public NetworkEntityGuid EntityGuid { get; }

		public DefaultEntityActorStateContainer(IEntityDataFieldContainer entityData, NetworkEntityGuid entityGuid)
		{
			EntityData = entityData ?? throw new ArgumentNullException(nameof(entityData));
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
