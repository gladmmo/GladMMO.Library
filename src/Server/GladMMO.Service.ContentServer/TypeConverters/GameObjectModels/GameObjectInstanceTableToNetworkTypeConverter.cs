using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public sealed class GameObjectInstanceTableToNetworkTypeConverter : ITypeConverterProvider<GameObjectEntryModel, GameObjectInstanceModel>
	{
		public GameObjectInstanceModel Convert([NotNull] GameObjectEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(0) //0 means that it's not an instance.
				.WithType(EntityType.Creature)
				.WithEntryId(fromObject.GameObjectEntryId)
				.Build();

			//TODO: better handle position crap
			return new GameObjectInstanceModel(guid, fromObject.GameObjectTemplateId, new Vector3(fromObject.SpawnPosition.X, fromObject.SpawnPosition.Y, fromObject.SpawnPosition.Z), fromObject.InitialOrientation);
		}
	}
}
