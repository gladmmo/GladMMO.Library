using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;
using UnityEngine;

namespace GladMMO
{
	
	public sealed class WorldTeleporterInstanceNetworkToTableTypeConverter : ITypeConverterProvider<WorldTeleporterInstanceModel, GameObjectWorldTeleporterEntryModel>
	{
		public GameObjectWorldTeleporterEntryModel Convert([NotNull] WorldTeleporterInstanceModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new GameObjectWorldTeleporterEntryModel(fromObject.TargetGameObjectId, fromObject.LocalSpawnPointId, fromObject.RemoteSpawnPointId);
		}
	}
}
