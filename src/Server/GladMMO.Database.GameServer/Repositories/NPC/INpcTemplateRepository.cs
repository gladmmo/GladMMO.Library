using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that implement data access as a repository to
	/// <see cref="NPCTemplateModel"/> data.
	/// </summary>
	public interface INpcTemplateRepository : IGenericRepositoryCrudable<int, NPCTemplateModel>, INameQueryableRepository<int>
	{

	}
}
