using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICreatureEntryRepository : IGenericRepositoryCrudable<int, CreatureEntryModel>, IInstanceableWorldObjectRepository<CreatureEntryModel>
	{
		//TODO: Doc
		Task<IReadOnlyCollection<CreatureTemplateEntryModel>> RetrieveTemplatesByWorldIdAsync(int worldId);
	}
}
