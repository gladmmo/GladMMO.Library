using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IGameObjectEntryRepository : IGenericRepositoryCrudable<int, GameObjectEntryModel>, IInstanceableWorldObjectRepository<GameObjectEntryModel>
	{

	}
}
