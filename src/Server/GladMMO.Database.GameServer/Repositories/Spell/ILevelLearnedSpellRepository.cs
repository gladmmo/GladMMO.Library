using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ILevelLearnedSpellRepository : IGenericRepositoryCrudable<int, SpellLevelLearned>
	{
		Task<SpellLevelLearned[]> RetrieveAllAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
