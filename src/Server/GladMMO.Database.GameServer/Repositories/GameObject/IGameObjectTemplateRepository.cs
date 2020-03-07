using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that implement data access as a repository to
	/// <see cref="GameObjectTemplateEntryModel"/> data.
	/// </summary>
	public interface IGameObjectTemplateRepository : IGenericRepositoryCrudable<int, GameObjectTemplateEntryModel>, INameQueryableRepository<int>, ITemplateableWorldObjectRepository<GameObjectTemplateEntryModel>
	{

	}
}
