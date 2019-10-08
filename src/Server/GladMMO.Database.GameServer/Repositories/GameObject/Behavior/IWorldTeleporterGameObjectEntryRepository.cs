using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IWorldTeleporterGameObjectEntryRepository : IGenericRepositoryCrudable<int, GameObjectWorldTeleporterEntryModel>, IInstanceableWorldObjectRepository<GameObjectWorldTeleporterEntryModel>
	{

	}
}
