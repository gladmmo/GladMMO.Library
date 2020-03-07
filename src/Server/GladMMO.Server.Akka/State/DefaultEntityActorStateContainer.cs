using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultEntityActorStateContainer : IEntityActorStateContainable
	{
		public IEntityDataFieldContainer EntityData { get; }

		public ObjectGuid EntityGuid { get; }

		public DefaultEntityActorStateContainer(IEntityDataFieldContainer entityData, ObjectGuid entityGuid)
		{
			EntityData = entityData ?? throw new ArgumentNullException(nameof(entityData));
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
