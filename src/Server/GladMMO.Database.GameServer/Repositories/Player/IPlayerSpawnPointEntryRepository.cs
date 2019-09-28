using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IPlayerSpawnPointEntryRepository : IGenericRepositoryCrudable<int, PlayerSpawnPointEntryModel>, IInstanceableWorldObjectRepository<PlayerSpawnPointEntryModel>
	{

	}
}
