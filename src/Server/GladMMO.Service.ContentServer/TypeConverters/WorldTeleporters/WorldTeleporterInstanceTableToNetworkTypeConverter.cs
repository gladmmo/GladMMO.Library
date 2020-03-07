using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public sealed class WorldTeleporterInstanceTableToNetworkTypeConverter : ITypeConverterProvider<GameObjectWorldTeleporterEntryModel, WorldTeleporterInstanceModel>
	{
		public WorldTeleporterInstanceModel Convert([NotNull] GameObjectWorldTeleporterEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));
			
			return new WorldTeleporterInstanceModel(fromObject.TargetGameObjectId, fromObject.LocalSpawnPointId, fromObject.RemoteSpawnPointId);
		}
	}
}
