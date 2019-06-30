using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public sealed class CreatureInstanceTableToNetworkTypeConverter : ITypeConverterProvider<CreatureEntryModel, CreatureInstanceModel>
	{
		public CreatureInstanceModel Convert([NotNull] CreatureEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(0) //0 means that it's not an instance.
				.WithType(EntityType.Npc)
				.WithEntryId(fromObject.CreatureEntryId)
				.Build();

			//TODO: better handle position crap
			return new CreatureInstanceModel(guid, fromObject.CreatureTemplateId, new Vector3(fromObject.SpawnPosition.X, fromObject.SpawnPosition.Y, fromObject.SpawnPosition.Z), fromObject.InitialOrientation);
		}
	}
}
