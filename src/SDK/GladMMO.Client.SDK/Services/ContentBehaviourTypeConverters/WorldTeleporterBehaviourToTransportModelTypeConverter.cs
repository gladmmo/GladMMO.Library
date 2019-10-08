using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.SDK
{
	public sealed class WorldTeleporterBehaviourToTransportModelTypeConverter : ITypeConverterProvider<WorldTeleporterDefinitionData, WorldTeleporterInstanceModel>
	{
		public WorldTeleporterInstanceModel Convert([NotNull] WorldTeleporterDefinitionData fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new WorldTeleporterInstanceModel(fromObject.gameObject.GetComponent<GameObjectStaticSpawnPointDefinition>().GameObjectInstanceId, fromObject.LocalSpawnPointId.PlayerSpawnPointId, fromObject.RemoteSpawnPointId);
		}
	}
}
