using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerSpawnPointInstanceTableToNetworkTypeConverter : ITypeConverterProvider<PlayerSpawnPointEntryModel, PlayerSpawnPointInstanceModel>
	{
		public PlayerSpawnPointInstanceModel Convert([NotNull] PlayerSpawnPointEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new PlayerSpawnPointInstanceModel(fromObject.PlayerSpawnId, new Vector3(fromObject.SpawnPosition.X, fromObject.SpawnPosition.Y, fromObject.SpawnPosition.Z), fromObject.InitialOrientation, fromObject.isReserved, fromObject.WorldId);
		}
	}
}
