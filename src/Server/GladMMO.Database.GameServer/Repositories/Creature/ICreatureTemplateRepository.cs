using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that implement data access as a repository to
	/// <see cref="CreatureTemplateEntryModel"/> data.
	/// </summary>
	public interface ICreatureTemplateRepository : IGenericRepositoryCrudable<int, CreatureTemplateEntryModel>, INameQueryableRepository<int>, ITemplateableWorldObjectRepository<CreatureTemplateEntryModel>
	{

	}
}
