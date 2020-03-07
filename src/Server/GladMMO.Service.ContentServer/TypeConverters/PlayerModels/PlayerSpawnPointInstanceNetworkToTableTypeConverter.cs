using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;
using UnityEngine;

namespace GladMMO
{
	
	public sealed class PlayerSpawnPointInstanceNetworkToTableTypeConverter : ITypeConverterProvider<PlayerSpawnPointInstanceModel, PlayerSpawnPointEntryModel>
	{
		public PlayerSpawnPointEntryModel Convert([NotNull] PlayerSpawnPointInstanceModel fromObject)
		{
			if(fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			//TODO: Kinda hacky, we don't have a valid world id.
			//TODO: better handle position crap
			return new PlayerSpawnPointEntryModel(new GladMMO.Database.Vector3<float>(fromObject.InitialPosition.x, fromObject.InitialPosition.y, fromObject.InitialPosition.z), fromObject.YAxisRotation, fromObject.SpawnPointId, fromObject.isReserved);
		}
	}
}
