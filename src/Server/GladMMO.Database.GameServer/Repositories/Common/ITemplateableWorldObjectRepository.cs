using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ITemplateableWorldObjectRepository<TTemplateModelType>
	{
		//TODO: Doc
		Task<IReadOnlyCollection<TTemplateModelType>> RetrieveTemplatesByWorldIdAsync(int worldId);
	}
}
